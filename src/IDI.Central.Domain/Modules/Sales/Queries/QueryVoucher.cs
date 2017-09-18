using System;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Sales.AggregateRoots;
using IDI.Central.Models.Sales;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Sales.Queries
{
    public class QueryVoucherCondition : Condition
    {
        public Guid Id { get; set; }
    }

    public class QueryVoucher : Query<QueryVoucherCondition, VoucherModel>
    {
        [Injection]
        public IQueryableRepository<Voucher> Vouchers { get; set; }

        public override Result<VoucherModel> Execute(QueryVoucherCondition condition)
        {
            var voucher = this.Vouchers.Include(e => e.Order).Find(condition.Id);

            if (voucher == null)
                return Result.Fail<VoucherModel>(Resources.Key.Command.RecordNotExisting);

            var model = new VoucherModel
            {
                Id = voucher.Id,
                TN = voucher.TN,
                Status = voucher.Status,
                PaymentAmount = voucher.Payment,
                PayMethod = voucher.PayMethod,
                Date = voucher.Date.AsLongDate(),
                PayableAmount = voucher.Payable,
                OrderId = voucher.OrderId,
                SN = voucher.Order.SN,
                Remark = voucher.Remark,
                Document = voucher.Document.AsBase64(voucher.ContentType),
            };

            return Result.Success(model);
        }
    }
}
