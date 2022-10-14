using OpenQA.Selenium;
using System.Collections;

namespace EATestFramework.Extensions
{
    public class TableDataCollection
    {
        public int RowNumber { get; set; }
        public string? ColumnName { get; set; }
        public string? ColumnValue { get; set; }
        public ColumnSpecialValue? ColumnSpecialValue { get; set; }
    }

    public class ColumnSpecialValue
    {
        public IEnumerable<IWebElement>? ElementCollection { get; set; }
        public ControlType? ControlType { get; set; }
    }

    public enum ControlType
    {
        hyperlink,
        input,
        option,
        select
    }

    public static class HtmlTableExtension
    {
        public static void PerformActionOnCell(this IWebElement element, string targetColumnIndex, 
            string refColumnName, string refColumnValue, string controlToOperate = null)
        {
            var table = ReadTable(element);

            foreach (int rowNumber in GetDynamicRowNumber(table, refColumnName, refColumnValue))
            {
                var cell = (from e in table
                            where e.ColumnName == targetColumnIndex && e.RowNumber == rowNumber
                            select e.ColumnSpecialValue).SingleOrDefault();

                if (controlToOperate != null && cell != null)
                {
                    IWebElement? elementToClick = null;
                    if (cell.ControlType == ControlType.hyperlink)
                    {
                        elementToClick = (from c in cell.ElementCollection
                                               where c.Text == controlToOperate.ToString()
                                               select c).SingleOrDefault();

                    }
                    if (cell.ControlType == ControlType.input)
                    {
                        elementToClick = (from c in cell.ElementCollection
                                          where c.GetAttribute("value") == controlToOperate.ToString()
                                          select c).SingleOrDefault();

                    }

                    elementToClick?.Click();
                }
                else
                {
                    cell.ElementCollection?.First().Click();
                }
            }
        }

        private static List<TableDataCollection> ReadTable(IWebElement table)
        {
            var tableDataCollection = new List<TableDataCollection>();

            var columns = table.FindElements(By.TagName("th"));
            var rows = table.FindElements(By.TagName("tr"));

            int rowIndex = 0;
            foreach (var row in rows)
            {
                int colIndex = 0;

                var colDatas = row.FindElements(By.TagName("td"));
                if (colDatas.Count != 0)
                {
                    foreach (var colValue in colDatas)
                    {
                        tableDataCollection.Add(new TableDataCollection
                        {
                            RowNumber = rowIndex,
                            ColumnName = columns[colIndex].Text != "" ?
                                        columns[colIndex].Text : colIndex.ToString(),
                            ColumnValue = colValue.Text,
                            ColumnSpecialValue = GetControl(colValue)
                        });

                        colIndex++;
                    }
                }

                rowIndex++;
            }

            return tableDataCollection;
        }

        private static ColumnSpecialValue? GetControl(IWebElement columnValue)
        {
            ColumnSpecialValue? columnSpecialValue = null;
            if (columnValue.FindElements(By.TagName("a")).Count > 0)
            {
                columnSpecialValue = new ColumnSpecialValue
                {
                    ElementCollection = columnValue.FindElements(By.TagName("a")),
                    ControlType = ControlType.hyperlink
                };
            }
            if (columnValue.FindElements(By.TagName("input")).Count > 0)
            {
                columnSpecialValue = new ColumnSpecialValue
                {
                    ElementCollection = columnValue.FindElements(By.TagName("input")),
                    ControlType = ControlType.input
                };
            }

            return columnSpecialValue;
        }

        public static IEnumerable GetDynamicRowNumber(List<TableDataCollection> tableCollection, 
            string columnName, string columnValue)
        {
            foreach (var table in tableCollection)
            {
                if (table.ColumnName == columnName && table.ColumnValue == columnValue)
                    yield return table.RowNumber;
            }
        }
    }
}
