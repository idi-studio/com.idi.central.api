using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IDI.Central.Domain.Common;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Central.Models.Retailing;
using IDI.Core.Common;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Retailing.Queries
{
    public class QueryProductCondition : Condition
    {
        public Guid Id { get; set; }

        [RequiredField]
        public string Domain { get; set; }

        [RequiredField]
        public string SavePath { get; set; }
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

            var task = GenerateAsync(condition, product.Pictures);

            return Result.Success(model);
        }

        private async Task GenerateAsync(QueryProductCondition condition, List<ProductPicture> pictures)
        {
            var path = Path.Combine(condition.SavePath, "assets", "images", "products", condition.Id.AsCode());

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            await Task.Run(() =>
            {
                foreach (var picture in pictures)
                {
                    var filename = Path.Combine(path, picture.AssetName());

                    if (File.Exists(filename))
                        continue;

                    if (picture.Data == null || (picture.Data != null && picture.Data.Length == 0))
                        continue;

                    using (var stream = new FileStream(filename, FileMode.CreateNew))
                    {
                        stream.Write(picture.Data, 0, picture.Data.Length);
                        stream.Flush();
                    }
                }
            });
        }
    }
}
