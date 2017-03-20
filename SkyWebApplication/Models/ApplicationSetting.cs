using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyWebApplication.Models
{
    public class Setting
    {
        public int Id { get; set; }

        /// <summary>
        /// 网站名
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 版权
        /// </summary>
        public string Copyright { get; set; }

        /// <summary>
        /// 网站协议
        /// </summary>
        public string Protocol { get; set; }

        /// <summary>
        /// SEO Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// SEO Keywords
        /// </summary>
        public string Keywords { get; set; }

        /// <summary>
        /// SEO Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 上传地址
        /// </summary>
        public string UploadUrl { get; set; }

        /// <summary>
        /// 前缀
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// 统计js
        /// </summary>
        public string Statistics { get; set; }

        /// <summary>
        /// 账号锁定分钟
        /// </summary>
        public int LockedMinutes { get; set; }

        /// <summary>
        /// 密码错误次数
        /// </summary>
        public int FailedPassword { get; set; }

        /// <summary>
        /// 验证码等待秒数
        /// </summary>
        public int CodeSeconds { get; set; }

        /// <summary>
        /// 验证码有效时间
        /// </summary>

        public int CodeMinutes { get; set; }

        public byte[] RowVersion { get; set; }

        /// <summary>
        /// 邮件服务器主机
        /// </summary>
        public string EmailHost { get; set; }

        /// <summary>
        /// 邮件服务器端口
        /// </summary>
        public int EmailPort { get; set; }

        /// <summary>
        /// 发件人
        /// </summary>
        public string EmailFrom { get; set; }

        /// <summary>
        /// 是否指定用户
        /// </summary>
        public bool UseCredential { get; set; }

        /// <summary>
        /// 指定用户名
        /// </summary>
        public string EmailUser { get; set; }

        /// <summary>
        /// 指定密码
        /// </summary>
        public string EmailPassword { get; set; }

        /// <summary>
        /// 认证链接超时分钟
        /// </summary>
        public int ActiveMinutes { get; set; }

        /// <summary>
        /// 验证码邮件标题
        /// </summary>
        public string EmailCodeTitle { get; set; }

        /// <summary>
        /// 验证码邮件内容
        /// </summary>
        public string EmailCodeContent { get; set; }

        /// <summary>
        /// 激活邮箱邮件标题
        /// </summary>
        public string EmailLinkTitle { get; set; }

        /// <summary>
        /// 激活邮件邮件内容
        /// </summary>
        public string EmailLinkContent { get; set; }

        /// <summary>
        /// 重置密码邮件标题
        /// </summary>
        public string ResetLinkTitle { get; set; }

        /// <summary>
        /// 重置密码邮件内容
        /// </summary>
        public string ResetLinkContent { get; set; }
    }
}