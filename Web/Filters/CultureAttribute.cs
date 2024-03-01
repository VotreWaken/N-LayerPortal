using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;

namespace MultilingualSite.Filters
{
    // модификаторы доступа для каждого файла ресурсов установлены в public. 
    // У файлов ресурсов значение свойства «Custom Tool» равно «PublicResXFileCodeGenerator» - инструмент создания ресурсов.
    // Иначе файлы ресурсов не будут скомпилированы и доступны.
    // Build Action - Embedded Resource
    // Custom Tool Namespace - Resources.

    public class CultureAttribute : Attribute, IActionFilter
    {
       
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
          
        }

        // фильтр действий, который будет срабатывать при обращении к действиям контроллера и производить локализацию.
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string? cultureName = null;
            // Получаем куки из контекста, которые могут содержать установленную культуру
            var cultureCookie = filterContext.HttpContext.Request.Cookies["lang"];
            if (cultureCookie != null)
                cultureName = cultureCookie;
            else
                cultureName = "ru";

            // Список культур
            List<string> cultures = new List<string>() { "ru", "en", "ua", "fr" };
            if (!cultures.Contains(cultureName))
            {
                cultureName = "ru";
            }
            // CultureInfo.CreateSpecificCulture создает объект CultureInfo, 
            // который представляет определенный язык и региональные параметры, 
            // соответствующие заданному имени.
            // CurrentCulture - задает язык и региональные параметры для текущего потока.
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            // CurrentUICulture задает текущие язык и региональные параметры, используемые диспетчером ресурсов 
            // для поиска ресурсов, связанных с языком и региональными параметрами, во время выполнения.
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
            // После этого для локализации система будет выбирать нужный файл ресурсов.
        }
    }
}