﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SmoONE.DTOs
{
    // ******************************************************************
    // 文件版本： SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler
    // 创建时间： 2016/11
    // 主要内容：  成本中心详情传输对象,用于提供新增/更新所需的数据.继承IEntity，方便根据IEntity类型进行DataAnnotations验证,也方便以后扩展
    // ******************************************************************
    /// <summary>
    /// 成本中心详情传输对象,用于提供新增/更新所需的数据
    /// </summary>
    public class CCInputDto:IEntity
    {
        /// <summary>
        /// 成本中心ID
        /// </summary>
        [Key]
        [StringLength(maximumLength: 20)]
        [DisplayName("成本中心ID")]
        public string CC_ID { get; set; }

        /// <summary>
        /// 成本中心名称
        /// </summary>
        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "长度不能超过20")]
        [DisplayName("成本中心名称")]
        public string CC_Name { get; set; }

        /// <summary>
        /// 成本中心类型ID
        /// </summary>
        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "长度不能超过20")]
        [DisplayName("成本中心类型")]
        public string CC_TypeID { get; set; }


        /// <summary>
        /// 成本中心责任人
        /// </summary>
        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "长度不能超过20")]
        [DisplayName("成本中心责任人")]
        public string CC_LiableMan { get; set; }

        /// <summary>
        /// 成本中心所属部门
        /// </summary>
        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "长度不能超过20")]
        [DisplayName("成本中心所属部门")]
        public string CC_DepartmentID { get; set; }


        /// <summary>
        /// 开始时间
        /// </summary>
        [Required]
        [DisplayName("开始时间")]
        public DateTime? CC_StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required]
        [DisplayName("结束时间")]
        public DateTime? CC_EndDate { get; set; }

        /// <summary>
        /// 成本中心金额
        /// </summary>
        [Required]
        [DisplayName("成本中心金额")]
        public decimal CC_Amount { get; set; }

        /// <summary>
        /// 成本中心模板ID
        /// </summary>
        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "长度不能超过20")]
        [DisplayName("成本中心模板")]
        public string CC_TemplateID { get; set; }


        ///// <summary>
        ///// 成本中心是否激活
        ///// </summary>
        //[Required]
        //[DisplayName("激活状态")]
        //public IsActive? CC_IsActive { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        [StringLength(maximumLength: 20, ErrorMessage = "长度不能超过20")]
        [DisplayName("创建者")]
        public string CC_CreateUser { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        [StringLength(maximumLength: 20, ErrorMessage = "长度不能超过20")]
        [DisplayName("更新者")]
        public string CC_UpdateUser { get; set; }


    }
}
