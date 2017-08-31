﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IDI.Central.Common;
using IDI.Central.Domain.Common;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Repositories;
using Microsoft.AspNetCore.Http;

namespace IDI.Central.Domain.Modules.Retailing.Commands
{
    public class ProductPictureCommand : Command
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public int Sequence { get; set; }

        public ImageCategory Category { get; set; }

        public List<IFormFile> Files { get; set; } = new List<IFormFile>();

        [RequiredField(Resources.Key.DisplayName.SavePath, Group = VerificationGroup.Create)]
        public string SavePath { get; set; }
    }

    public class ProductPictureCommandHandler : CommandHandler<ProductPictureCommand>
    {
        private readonly string[] types = { "image/jpeg", "image/png" };
        private readonly string[] extensions = { ".png", ".jpg", ".jpge" };
        private readonly long maximum = 800;

        [Injection]
        public IRepository<Product> Products { get; set; }

        [Injection]
        public IRepository<ProductPicture> Pictures { get; set; }

        protected override Result Create(ProductPictureCommand command)
        {
            if (command.Files.Count == 0)
                return Result.Fail(Localization.Get(Resources.Key.Command.NoFileUpload));

            var product = this.Products.Include(e => e.Pictures).Find(command.ProductId);

            if (product == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.ProductNotExisting));

            int sequence = product.Pictures.Count == 0 ? 0 : product.Pictures.Max(e => e.Sequence);

            foreach (var file in command.Files)
            {
                long size = file.Length / 1024;

                if (size > maximum)
                    return Result.Fail(Localization.Get(Resources.Key.Command.FileMaxSizeLimit).ToFormat($"{maximum} KB"));

                if (!types.Any(e => e == file.ContentType))
                    return Result.Fail(Localization.Get(Resources.Key.Command.SupportedExtension).ToFormat(extensions.JoinToString(",")));

                var picture = new ProductPicture
                {
                    Sequence = sequence + 1,
                    Name = Path.GetFileNameWithoutExtension(file.FileName),
                    FileName = file.FileName.ToLower(),
                    Extension = Path.GetExtension(file.FileName),
                    ContentType = file.ContentType,
                    Category = command.Category,
                    ProductId = command.ProductId
                };

                Save(picture, command.SavePath, file);

                this.Pictures.Add(picture);

                sequence += 1;
            }

            this.Pictures.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.CreateSuccess));
        }

        protected override Result Update(ProductPictureCommand command)
        {
            var picture = this.Pictures.Find(command.Id);

            if (picture == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            picture.Sequence = command.Sequence;
            picture.Category = command.Category;

            this.Pictures.Update(picture);
            this.Pictures.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.UpdateSuccess));
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

        private void Save(ProductPicture picture, string savePath, IFormFile file)
        {
            var path = Path.Combine(savePath, "assets", "images", picture.ResourceName());

            using (var stream = new FileStream(path, FileMode.CreateNew))
            {
                file.CopyTo(stream);
                stream.Flush();
            }
        }
    }
}