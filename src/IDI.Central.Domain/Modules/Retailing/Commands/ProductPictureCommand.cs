using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Repositories;
using Microsoft.AspNetCore.Http;

namespace IDI.Central.Domain.Modules.Retailing.Commands
{
    public class ProductPictureCommand : Command
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
    }

    public class ProductPictureCommandHandler : CommandHandler<ProductPictureCommand>
    {
        private readonly string[] extensions = { "png", "jpg", "jpge" };
        private readonly long maximum = 800;

        [Injection]
        public IRepository<Product> Products { get; set; }

        [Injection]
        public IRepository<ProductPicture> Pictures { get; set; }

        protected override Result Create(ProductPictureCommand command)
        {
            if (command.Files.Count == 0)
                return Result.Fail(Localization.Get(Resources.Key.Command.NoFileUpload));

            if (!this.Products.Exist(e => e.Id == command.Id))
                return Result.Fail(Localization.Get(Resources.Key.Command.ProductNotExisting));

            foreach (var file in command.Files)
            {
                long size = file.Length / 1024;

                if (size > maximum)
                    return Result.Fail(Localization.Get(Resources.Key.Command.FileMaxSizeLimit).ToFormat($"{maximum} KB"));

                var picture = new ProductPicture
                {
                    Name = file.Name,
                    FileName = file.FileName,
                    Extension = Path.GetExtension(file.FileName),
                };

                if (!extensions.Any(e => e == picture.Extension))
                    return Result.Fail(Localization.Get(Resources.Key.Command.SupportedExtension).ToFormat(extensions.JoinToString(",")));

                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    picture.Image = ms.GetBuffer();
                    ms.Flush();
                }

                this.Pictures.Add(picture);
            }

            this.Pictures.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.CreateSuccess));
        }

        protected override Result Update(ProductPictureCommand command)
        {
            return Result.Fail(message: Localization.Get(Resources.Key.Command.OperationNonsupport));
        }

        protected override Result Delete(ProductPictureCommand command)
        {
            var picture = this.Pictures.Find(command.Id);

            if (picture == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            this.Pictures.Remove(picture);
            this.Pictures.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.DeleteSuccess));
        }
    }
}
