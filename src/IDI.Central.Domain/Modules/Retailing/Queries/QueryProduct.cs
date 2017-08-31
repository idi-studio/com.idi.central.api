﻿using System;
using System.Linq;
using System.Collections.Generic;
using IDI.Central.Domain.Common;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Central.Models.Retailing;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Retailing.Queries
{
    public class QueryProductCondition : Condition
    {
        public Guid Id { get; set; }

        public string Domain { get; set; }
    }

    public class QueryProduct : Query<QueryProductCondition, ProductModel>
    {
        [Injection]
        public IQueryableRepository<Product> Products { get; set; }

        public override Result<ProductModel> Execute(QueryProductCondition condition)
        {
            var product = this.Products.Include(e => e.Pictures).Find(condition.Id);

            var url = $"{condition.Domain}/assets/images/products";

            var model = new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                QRCode = product.QRCode,
                Description = product.Tags.To<List<Tag>>().AsString(),
                Tags = product.Tags.To<List<Tag>>(),
                Enabled = product.Enabled,
                OnShelf = product.OnShelf,
                Pictures = product.Pictures.Select(e => new ProductPictureModel
                {
                    Id = e.Id,
                    ProductId = e.ProductId,
                    Sequence = e.Sequence,
                    Name = e.Name,
                    Category = e.Category,
                    FileName = e.FileName,
                    Date = e.CreatedAt.AsLongDate(),
                    URL = $"{url}/{e.ProductId.AsCode()}/{e.AssetName()}"
                }).OrderBy(e => e.Category).ThenBy(e => e.Sequence).ToList(),
            };

            return Result.Success(model);
        }
    }
}
