using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Framework
{
    class WebTable
    {
        private IList<IWebElement> Rows;
        private IList<IWebElement> Headers;
        private IWebElement table;
        private bool hasHeaderRow;

        private bool HasHeaderRow
        {
            get
            {
                return hasHeaderRow;
            }
            set
            {
                hasHeaderRow = value;
                if (value)
                {
                    Headers = Rows[0].FindElements(By.XPath("./*"));
                } else
                {
                    Headers = null;
                }
            }
        }

        public WebTable(IWebElement table)
        {
            this.table = table;
            Rows = table.FindElements(By.XPath("./tr"));
            IList<IWebElement> Cols = Rows[0].FindElements(By.XPath("./*"));
            if (Cols[0].TagName.ToLower().Equals("<th>"))
            {
                HasHeaderRow = true;
            }
            else
            {
                HasHeaderRow = false;
            }
        }

        public string GetCellValue(int row, int col)
        {
            IList<IWebElement> Cols = Rows[row].FindElements(By.XPath("./*"));
            return Cols[col].Text;
        }

        public string GetCellValue(int row, string colName)
        {
            string cellValue = "";

            if (HasHeaderRow)
            {
                for (int cnt = 0; cnt <= Headers.Count; cnt++)
                {
                    if (Headers[cnt].Text.ToLower().Equals(colName.ToLower()))
                    {
                        IList<IWebElement> Cols = Rows[row].FindElements(By.XPath("./*"));
                        cellValue = Cols[cnt].Text;
                        break;
                    }
                }
            } else { throw InvalidOperationException; }

            return cellValue;
        }

        public string GetCellValue(string row, int searchCol, int returnCol)
        {

        }

        public string GetCellValue(string row, string col)
        {
            string cellValue = "";
            return cellValue;
        }
    }
}
