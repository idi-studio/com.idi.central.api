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

        public TradeStatus Status { get; set; }

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

            if (voucher != null)
                return Result.Success(message: Localization.Get(Resources.Key.Command.Success)).Attach("vchrid", voucher.Id);

            var order = this.Orders.Include(e => e.Items).Find(e => e.Id == command.OrderId);

            if (order == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.InvalidOrder));

            if (order.Status != OrderStatus.Confirmed)
                return Result.Fail(Localization.Get(Resources.Key.Command.PlsConfirmOrder));

            DateTime timestamp = DateTime.Now;

            var amount = order.Items.Sum(e => e.UnitPrice * e.Quantity);

            voucher = new Voucher
            {
                TN = GenerateTransNo(timestamp),
                Status = TradeStatus.InProcess,
                Date = timestamp,
                OrderId = order.Id,
                PayMethod = PayMethod.Other,
                PayableAmount = amount,
                PaymentAmount = amount
            };

            this.Vouchers.Add(voucher);
            this.Vouchers.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.CreateSuccess)).Attach("vchrid", voucher.Id);
        }

        protected override Result Update(VoucherCommand command)
        {
            var voucher = this.Vouchers.Find(command.Id);

            if (voucher == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            bool processed = false;

            processed |= Save(voucher, command);
            processed |= Paid(voucher, command);

            if (processed)
            {
                this.Vouchers.Update(voucher);
                this.Vouchers.Commit();

                return Result.Success(message: Localization.Get(Resources.Key.Command.OperationSuccess));
            }

            return Result.Success(message: Localization.Get(Resources.Key.Command.OperationFail));
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

            Attach(voucher, command.File);

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

        private string GenerateTransNo(DateTime timestamp)
        {
            return $"{timestamp.ToString("yyMMdd")}{timestamp.TimeOfDay.Ticks.ToString("x8")}".ToUpper();
        }

        private bool Save(Voucher voucher, VoucherCommand command)
        {
            if (!(voucher.Status == TradeStatus.InProcess && command.Status == TradeStatus.InProcess))
                return false;

            voucher.PayMethod = command.PayMethod;
            voucher.PaymentAmount = command.PayAmount;
            voucher.Remark = command.Remark;

            return true;
        }

        private bool Paid(Voucher voucher, VoucherCommand command)
        {
            if (!(voucher.Status == TradeStatus.InProcess && command.Status == TradeStatus.Success))
                return false;

            voucher.Status = TradeStatus.Success;

            return true;
        }

        private void Attach(Voucher voucher, IFormFile file)
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
