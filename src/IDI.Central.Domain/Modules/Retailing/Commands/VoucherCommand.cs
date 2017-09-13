using System;
using System.IO;
using System.Linq;
using IDI.Central.Common.Enums;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Repositories;
using Microsoft.AspNetCore.Http;

namespace IDI.Central.Domain.Modules.Retailing.Commands
{
    public class VoucherCommand : Command
    {
        public Guid Id { get; set; }

        public PayMethod PayMethod { get; set; }

        public decimal PayAmount { get; set; }

        public string Remark { get; set; }

        public Guid OrderId { get; set; }

        public IFormFile File { get; set; }
    }

    public class VoucherCommandHandler : CommandHandler<VoucherCommand>
    {
        private readonly string[] types = { "image/jpeg", "image/png" };
        private readonly string[] extensions = { ".png", ".jpg", ".jpge" };
        private readonly long maximum = 800;

        [Injection]
        public IRepository<Order> Orders { get; set; }

        [Injection]
        public IRepository<Voucher> Vouchers { get; set; }

        protected override Result Create(VoucherCommand command)
        {
            var voucher = this.Vouchers.Find(e => e.OrderId == command.OrderId);

            if (voucher == null)
            {
                var order = this.Orders.Include(e => e.Items).Find(e => e.Id == command.OrderId);

                if (order == null)
                    return Result.Fail(Localization.Get(Resources.Key.Command.InvalidOrder));

                if (order.Status != OrderStatus.Confirmed)
                    return Result.Fail(Localization.Get(Resources.Key.Command.PlsConfirmOrder));

                DateTime timestamp = DateTime.Now;

                var amount = order.Items.Sum(e => e.UnitPrice * e.Quantity);

                voucher = new Voucher
                {
                    TN = GenerateTransactionNumber(timestamp),
                    Date = timestamp,
                    OrderId = order.Id,
                    PayMethod = PayMethod.Other,
                    OrderAmount = amount,
                    PayAmount = amount
                };

                this.Vouchers.Add(voucher);
                this.Vouchers.Commit();
            }

            return Result.Success(message: Localization.Get(Resources.Key.Command.CreateSuccess)).Attach("vchrid", voucher.Id);
        }

        protected override Result Update(VoucherCommand command)
        {
            var voucher = this.Vouchers.Find(command.Id);

            if (voucher == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            if (command.File != null)
            {
                Save(voucher, command.File);
            }
            else
            {
                voucher.PayMethod = command.PayMethod;
                voucher.PayAmount = command.PayAmount;
                voucher.Remark = command.Remark;
            }

            this.Vouchers.Update(voucher);
            this.Vouchers.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.UpdateSuccess));
        }

        protected override Result Upload(VoucherCommand command)
        {
            if (command.File == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.NoFileUpload));

            var voucher = this.Vouchers.Find(command.Id);

            if (voucher == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            long size = command.File.Length / 1024;

            if (size > maximum)
                return Result.Fail(Localization.Get(Resources.Key.Command.FileMaxSizeLimit).ToFormat($"{maximum} KB"));

            if (!types.Any(e => e == command.File.ContentType))
                return Result.Fail(Localization.Get(Resources.Key.Command.SupportedExtension).ToFormat(extensions.JoinToString(",")));

            Save(voucher, command.File);

            this.Vouchers.Update(voucher);
            this.Vouchers.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.UploadSuccess));
        }

        protected override Result Delete(VoucherCommand command)
        {
            var voucher = this.Vouchers.Find(command.Id);

            if (voucher == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            this.Vouchers.Remove(voucher);
            this.Vouchers.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.DeleteSuccess));
        }

        private string GenerateTransactionNumber(DateTime timestamp)
        {
            return $"{timestamp.ToString("yyMMdd")}{timestamp.TimeOfDay.Ticks.ToString("x8")}".ToUpper();
        }

        private void Save(Voucher voucher, IFormFile file)
        {
            using (var memory = new MemoryStream())
            {
                file.CopyTo(memory);

                voucher.ContentType = file.ContentType;
                voucher.Document = memory.GetBuffer();

                memory.Flush();
            }
        }
    }
}
