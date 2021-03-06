﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SkyWebApplication.Models;

namespace SkyWebApplication.DAL
{
    public class SkyWebInitializer :DropCreateDatabaseIfModelChanges<SkyWebContext>
    {
        protected override void Seed(SkyWebContext context)
        {
            base.Seed(context);

            #region 初始化配置

            var protocol = new System.Text.StringBuilder(942);
            protocol.AppendLine(@"<p> 当您申请用户时，表示您已经同意遵守本规章。");
            protocol.AppendLine(@"    欢迎您加入本站点参加交流和讨论，本站点为公共论坛，为维护网上公共秩序和社会稳定，请您自觉遵守以下条款：</p>");
            protocol.AppendLine(@"   <ul> ");
            protocol.AppendLine(@"       <li>一、不得利用本站危害国家安全、泄露国家秘密，不得侵犯国家社会集体的和公民的合法权益，不得利用本站制作、复制和传播下列信息：");
            protocol.AppendLine(@"           <ol>");
            protocol.AppendLine(@"               <li>（一）煽动抗拒、破坏宪法和法律、行政法规实施的；</li>");
            protocol.AppendLine(@"               <li>（二）煽动颠覆国家政权，推翻社会主义制度的；</li>");
            protocol.AppendLine(@"               <li>（三）煽动分裂国家、破坏国家统一的；</li>");
            protocol.AppendLine(@"               <li>（四）煽动民族仇恨、民族歧视，破坏民族团结的；</li>");
            protocol.AppendLine(@"               <li>（五）捏造或者歪曲事实，散布谣言，扰乱社会秩序的；</li>");
            protocol.AppendLine(@"               <li>（六）宣扬封建迷信、淫秽、色情、赌博、暴力、凶杀、恐怖、教唆犯罪的；</li>");
            protocol.AppendLine(@"               <li>（七）公然侮辱他人或者捏造事实诽谤他人的，或者进行其他恶意攻击的；</li>");
            protocol.AppendLine(@"               <li>（八）损害国家机关信誉的；</li>");
            protocol.AppendLine(@"               <li>（九）其他违反宪法和法律行政法规的；</li>");
            protocol.AppendLine(@"               <li>（十）进行商业广告行为的。</li>");
            protocol.AppendLine(@"           </ol>");
            protocol.AppendLine(@"       </li>");
            protocol.AppendLine(@"    <li>二、互相尊重，对自己的言论和行为负责。</li>");
            protocol.AppendLine(@"    <li>三、禁止在申请用户时使用相关本站的词汇，或是带有侮辱、毁谤、造谣类的或是有其含义的各种语言进行注册用户，否则我们会将其删除。</li>");
            protocol.AppendLine(@"    <li>四、禁止以任何方式对本站进行各种破坏行为。</li>");
            protocol.AppendLine(@"   <li> 五、如果您有违反国家相关法律法规的行为，本站概不负责，您的登录论坛信息均被记录无疑，必要时，我们会向相关的国家管理部门提供此类信息。</li>");
            protocol.AppendLine(@"   </ul>");

            var copyright = new System.Text.StringBuilder(143);
            copyright.AppendLine(@"Copyright©2008-2011 赵征 Co.,Ltd, All Rights Reserved 通灵帕克 版权所有");
            copyright.AppendLine(@"增值电信业务经营许可证京-2-4-20090003 广播电视节目制作经营许可证 文网文 [2009]011号");

            context.Settings.Add(new Setting
            {
                SiteName = "SkyWeb",
                DomainName = "http://www.zzd123.com",
                Logo = "Upload/Site/logo.jpg",
                Protocol = protocol.ToString(),
                Title = "SkyWeb",
                Keywords = "SkyWeb",
                Description = "SkyWeb",
                Copyright = copyright.ToString(),
                Statistics = string.Empty,

                FileUploadUrl = "~/File/Upload/",
                ImgUploadUrl = "~/File/Upload/",
                EditorUploadUrl = "~/File/Upload/",
                AvatarUploadUrl = "~/File/Upload/",
                BaseUrl = "~",

               
                
                FailedPassword = 5,
                CodeSeconds = 60,
                CodeMinutes = 2,
                LockedMinutes = 5,


                EmailHost = "smtp.126.com",
                EmailPort = 25,
                EmailFrom = "ccvixo@126.com",
                EmailUser = "ccvixo",
                EmailPassword = "1qaz2wsx",
                ActiveMinutes = 30,
                EmailCodeTitle = "验证码邮件",
                EmailCodeContent = "<p>您的验证码是:</p><span style=\"font-size:14px;\">{0}</span>",
                EmailLinkTitle = "激活邮件",
                EmailLinkContent = "<p>点击链接激活:</p><a href=\"{0}\">链接：{1}</a>",
                ResetLinkTitle = "重置邮件",
                ResetLinkContent = "<p>点击链接重置:</p><a href=\"{0}\">链接：{1}</a>",


            });

            context.SaveChanges();

            #endregion 初始化配置


            #region 用户角色初始化
            var sysUsers = new List<SysUser>
            {
                new SysUser{UserName="Tom",Email="Tom@163.com",Password="1",Status=true},
                new SysUser{UserName="Jerry",Email="Jerry@163.com",Password="2",Status=true},
                new SysUser{UserName="Jeem",Email="Jeem@163.com",Password="2",Status=false}
            };
            sysUsers.ForEach(s => context.SysUsers.Add(s));
            context.SaveChanges();



            var sysRoles = new List<SysRole>
            {
                new SysRole{RoleName="超级管理员",RoleDesc="超级管理员"},
                new SysRole{RoleName="管理员",RoleDesc="管理员"}
            };
            sysRoles.ForEach(s => context.SysRoles.Add(s));
            context.SaveChanges();

            var SysUserRoles = new List<SysUserRole>
            {
                new SysUserRole{SysUserID=1,SysRoleID=1},
                new SysUserRole{SysUserID=1,SysRoleID=2},
                new SysUserRole{SysUserID=2,SysRoleID=1},
                new SysUserRole{SysUserID=3,SysRoleID=2}

            };
            SysUserRoles.ForEach(s => context.SysUserRoles.Add(s));
            context.SaveChanges();

            var SkyAuthorizes = new List<SkyAuthorize>
            {
                new SkyAuthorize{SkyControllerName="Home",SkyActionName="Index",SkyLinkTitle="首页",SkyRolesID="超级管理员,管理员"},
                new SkyAuthorize{SkyControllerName="Home",SkyActionName="About",SkyLinkTitle="关于我们",SkyRolesID="管理员"},
  

            };
            SkyAuthorizes.ForEach(s => context.SkyAuthorizes.Add(s));
            context.SaveChanges();


            #endregion


            #region 字典，分类，部门初始化
            var categorys = new List<Category>
            {
                new Category{CategoryName="顶级分类",CategoryInfo="顶级分类",CategoryParentID=0,CategoryStatus=true,CategorySort=0},

                new Category{CategoryName="文章类型",CategoryInfo="文章类型说明",CategoryParentID=1,CategoryStatus=true,CategorySort=0},
                new Category{CategoryName="图片类型",CategoryInfo="图片类型说明",CategoryParentID=1,CategoryStatus=true,CategorySort=0},


                new Category{CategoryName="通知公告",CategoryInfo="通知公告说明",CategoryParentID=2,CategoryStatus=true,CategorySort=0},
                new Category{CategoryName="新闻资讯",CategoryInfo="新闻资讯说明",CategoryParentID=2,CategoryStatus=true,CategorySort=0},

                new Category{CategoryName="师资图片",CategoryInfo="师资说明",CategoryParentID=3,CategoryStatus=true,CategorySort=0},
                new Category{CategoryName="活动图片",CategoryInfo="活动说明",CategoryParentID=3,CategoryStatus=true,CategorySort=0},

                new Category{CategoryName="通知",CategoryInfo="通知说明",CategoryParentID=4,CategoryStatus=false,CategorySort=0},
                new Category{CategoryName="公告",CategoryInfo="公告说明",CategoryParentID=4,CategoryStatus=true,CategorySort=0},

            };
            categorys.ForEach(s => context.Categorys.Add(s));
            context.SaveChanges();

            var departments = new List<Department>
            {
                new Department{DepartmentName="部门架构",DepartmentInfo="部门架构",DepartmentParentID=0,DepartmentStatus=true,DepartmentSort=0},

                new Department{DepartmentName="公司名称",DepartmentInfo="公司名称",DepartmentParentID=1,DepartmentStatus=true,DepartmentSort=0},

                new Department{DepartmentName="销售部",DepartmentInfo="销售部",DepartmentParentID=2,DepartmentStatus=true,DepartmentSort=0},
                new Department{DepartmentName="研发部",DepartmentInfo="研发部",DepartmentParentID=2,DepartmentStatus=true,DepartmentSort=0},

                new Department{DepartmentName="基础医疗",DepartmentInfo="基础医疗",DepartmentParentID=3,DepartmentStatus=true,DepartmentSort=0},
                new Department{DepartmentName="公共卫生",DepartmentInfo="公共卫生",DepartmentParentID=3,DepartmentStatus=true,DepartmentSort=0},

                new Department{DepartmentName="网络研发",DepartmentInfo="网络研发",DepartmentParentID=4,DepartmentStatus=false,DepartmentSort=0},
                new Department{DepartmentName="药品研发",DepartmentInfo="药品研发",DepartmentParentID=4,DepartmentStatus=true,DepartmentSort=0},

            };
            departments.ForEach(s => context.Departments.Add(s));
            context.SaveChanges();

            var dictionarys = new List<Dictionary>
            {
                new Dictionary{DictionaryName="系统字典",DictionaryInfo="系统字典",DictionaryParentID=0,DictionaryStatus=true,DictionarySort=0},

                new Dictionary{DictionaryName="学历",DictionaryInfo="学历说明",DictionaryParentID=1,DictionaryStatus=true,DictionarySort=0},
                new Dictionary{DictionaryName="性别",DictionaryInfo="性别说明",DictionaryParentID=1,DictionaryStatus=true,DictionarySort=0},

                new Dictionary{DictionaryName="博士",DictionaryInfo="博士说明",DictionaryParentID=2,DictionaryStatus=true,DictionarySort=0},
                new Dictionary{DictionaryName="硕士",DictionaryInfo="硕士说明",DictionaryParentID=2,DictionaryStatus=true,DictionarySort=0},


                new Dictionary{DictionaryName="男",DictionaryInfo="通知说明",DictionaryParentID=3,DictionaryStatus=false,DictionarySort=0},
                new Dictionary{DictionaryName="女",DictionaryInfo="公告说明",DictionaryParentID=3,DictionaryStatus=true,DictionarySort=0},

            };
            dictionarys.ForEach(s => context.Dictionarys.Add(s));
            context.SaveChanges();

            #endregion 初始化配置
        }
    }
}