using System;
using System.Collections.Generic;

namespace IDI.Core.Tests.Common
{
    public class Contants
    {
        public class ConnectionStrings
        {
            public const string EFTestDb = @"Server=localhost; Database=com.idi.core.testing;User Id = sa; Password = p@55w0rd;";
        }

        public class TestCategory
        {
            public const string Verification = "Verification";
            public const string Command = "Command";
            public const string Language = "Language";
        }

        public class Tables
        {
            public const string Users = @"Users";
            public const string Blogs = @"Blogs";
            public const string Posts = @"Posts";
        }

        public class DbOperations
        {
            public class InsertUser
            {
                public const string PK = "D0A36A22-1D4E-4021-9207-D8ABEA48BE07";
                public const string UserName = "testuser";
                public const string Password = "123456";
                public const string CmdText = "INSERT INTO [Users]([Id],[CreatedBy],[CreatedTime],[LastUpdatedBy],[LastUpdatedTime],[Password],[UserName],[Version]) VALUES('D0A36A22-1D4E-4021-9207-D8ABEA48BE07','-', GETDATE(),'-',GETDATE(),'123456','testuser',1)";
            }

            public class InsertBlog
            {
                public const string PK = "6e1570f0-23d1-4f5a-9477-884c0918ac5c";
                public const string URL = "http://www.cnblogs.com/";
                public const string CmdText = "INSERT [dbo].[Blogs] ([Id], [CreatedBy], [CreatedTime], [LastUpdatedBy], [LastUpdatedTime], [Url], [Version]) VALUES (N'6e1570f0-23d1-4f5a-9477-884c0918ac5c', N'-', CAST(0x07EAEF032C83793C0B AS DateTime2), N'-', CAST(0x07EAEF032C83793C0B AS DateTime2), N'http://www.cnblogs.com/', 1)";
            }

            public class InsertPost
            {
                public const string FK = "6e1570f0-23d1-4f5a-9477-884c0918ac5c";
                public const string PK1 = "3448a42c-0aa0-4d3e-a2e6-c3932cb5371e";
                public const string PK2 = "e847640f-34f7-4344-8041-cdf869aba32c";
                public const string CmdText1 = "INSERT [dbo].[Posts] ([Id], [BlogId], [Content], [CreatedBy], [CreatedTime], [LastUpdatedBy], [LastUpdatedTime], [Title], [Version]) VALUES (N'3448a42c-0aa0-4d3e-a2e6-c3932cb5371e', N'6e1570f0-23d1-4f5a-9477-884c0918ac5c', N'TestContent1', N'-', CAST(0x07EAEF032C83793C0B AS DateTime2), N'-', CAST(0x07EAEF032C83793C0B AS DateTime2), N'TestTitle1', 1)";
                public const string CmdText2 = "INSERT [dbo].[Posts] ([Id], [BlogId], [Content], [CreatedBy], [CreatedTime], [LastUpdatedBy], [LastUpdatedTime], [Title], [Version]) VALUES (N'e847640f-34f7-4344-8041-cdf869aba32c', N'6e1570f0-23d1-4f5a-9477-884c0918ac5c', N'TestContent2', N'-', CAST(0x07EAEF032C83793C0B AS DateTime2), N'-', CAST(0x07EAEF032C83793C0B AS DateTime2), N'TestTitle2', 1)";
            }

            public class BatchInsertBlog
            {
                public static string[] BuildCmdTexts(int blogCount = 5, int postCount = 5)
                {
                    var cmdTexts = new List<string>();

                    for (int i = 0; i < blogCount; i++)
                    {
                        var blogKey = Guid.NewGuid();
                         var cmdText1 = $"INSERT [dbo].[Blogs] ([Id], [CreatedBy], [CreatedTime], [LastUpdatedBy], [LastUpdatedTime], [Url], [Version]) VALUES (N'{blogKey}', N'-', CAST(0x07EAEF032C83793C0B AS DateTime2), N'-', CAST(0x07EAEF032C83793C0B AS DateTime2), N'http://www.cnblogs.com/?id={i+1}', 1)";

                        cmdTexts.Add(cmdText1);

                        for (int j = 0; j < postCount; j++)
                        {
                            var postKey = Guid.NewGuid();
                            var cmdText2 = $"INSERT [dbo].[Posts] ([Id], [BlogId], [Content], [CreatedBy], [CreatedTime], [LastUpdatedBy], [LastUpdatedTime], [Title], [Version]) VALUES (N'{postKey}', N'{blogKey}', N'TestContent{i}{j}', N'-', CAST(0x07EAEF032C83793C0B AS DateTime2), N'-', CAST(0x07EAEF032C83793C0B AS DateTime2), N'TestTitle{i}{j}', 1)";
                            cmdTexts.Add(cmdText2);
                        }
                    }

                    return cmdTexts.ToArray();
                }
            }
        }
    }
}
