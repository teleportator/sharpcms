﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SharpCMS.UI.Mvc.Models.Vacancies
{
	public class VacancyCreateModel
	{
	    public string ParentUrl { get; set; }

        [DisplayName("Родительский раздел")]
        public string ParentTitle { get; set; }

        [StringLength(200, ErrorMessage = "Превышен допустимый лимит длины")]
        [Required(ErrorMessage = "Введите заголовок")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [AllowHtml]
        [DisplayName("Основной текст")]
        public string Text { get; set; }

        [StringLength(200, ErrorMessage = "Превышен допустимый лимит длины")]
        [Required(ErrorMessage = "Введите краткое содержание")]
        [DisplayName("Краткое содержание")]
        public string Abstract { get; set; }

        [DisplayName("Показывать на сайте")]
        public bool IsActive { get; set; }

        [Range(1, 1000, ErrorMessage = "Введите целое число от 1 до 1000")]
        [Required(ErrorMessage = "Введите порядок сортировки")]
        [DisplayName("Порядок сортировки")]
        public int SortOrder { get; set; }

        [DisplayName("Показывать в главном меню")]
        public bool DisplayOnMainMenu { get; set; }

        [DisplayName("Показывать в боковом меню")]
        public bool DisplayOnSideMenu { get; set; }

        [Required(ErrorMessage = "Введите работадателя")]
        [DisplayName("Работадатель")]
        public string Employer { get; set; }

        [Required(ErrorMessage = "Введите контакты")]
        [DisplayName("Контакты")]
        public string Contact { get; set; }

        [Required(ErrorMessage = "Введите обязанности")]
        [DisplayName("Обязанности")]
        public string Responsibilities { get; set; }

        [Required(ErrorMessage = "Введите требования")]
        [DisplayName("Требования")]
        public string Demands { get; set; }

        [Required(ErrorMessage = "Введите условия")]
        [DisplayName("Условия")]
        public string Conditions { get; set; }
	}
}