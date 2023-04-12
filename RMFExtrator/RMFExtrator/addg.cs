List<DataPointGridViewModel> m_dataGridBindingList = null;
List<DataPointGridViewModel> m_filteredList = null;

private void dataGridView2_FilterStringChanged(object sender, Zuby.ADGV.AdvancedDataGridView.FilterEventArgs e)
{
    try
    {
        if (string.IsNullOrEmpty(dataGridView2.FilterString) == true)
        {
            m_filteredList = m_dataGridBindingList;
            dataGridView2.DataSource = m_dataGridBindingList;
        }
        else
        {
            var listfilter = FilterStringconverter(dataGridView2.FilterString);

            m_filteredList = m_filteredList.Where(listfilter).ToList();

            dataGridView2.DataSource = m_filteredList;
        }
    }
    catch (Exception ex)
    {
        Log.Error(ex, MethodBase.GetCurrentMethod().Name);
    }
}

private void dataGridView2_SortStringChanged(object sender, Zuby.ADGV.AdvancedDataGridView.SortEventArgs e)
{
    try
    {
        if (string.IsNullOrEmpty(dataGridView2.SortString) == true)
            return;

        var sortStr = dataGridView2.SortString.Replace("[", "").Replace("]", "");

        if (string.IsNullOrEmpty(dataGridView2.FilterString) == true)
        {
            // the grid is not filtered!
            m_dataGridBindingList = m_dataGridBindingList.OrderBy(sortStr).ToList();
            dataGridView2.DataSource = m_dataGridBindingList;
        }
        else
        {
            // the grid is filtered!
            m_filteredList = m_filteredList.OrderBy(sortStr).ToList();
            dataGridView2.DataSource = m_filteredList;
        }
    }
    catch (Exception ex)
    {
        Log.Error(ex, MethodBase.GetCurrentMethod().Name);
    }
}


private string FilterStringconverter(string filter)
{
    string newColFilter = "";

    // get rid of all the parenthesis 
    filter = filter.Replace("(", "").Replace(")", "");

    // now split the string on the 'and' (each grid column)
    var colFilterList = filter.Split(new string[] { "AND" }, StringSplitOptions.None);

    string andOperator = "";

    foreach (var colFilter in colFilterList)
    {
        newColFilter += andOperator;

        // split string on the 'in'
        var temp1 = colFilter.Trim().Split(new string[] { "IN" }, StringSplitOptions.None);

        // get string between square brackets
        var colName = temp1[0].Split('[', ']')[1].Trim();

        // prepare beginning of linq statement
        newColFilter += string.Format("({0} != null && (", colName);

        string orOperator = "";

        var filterValsList = temp1[1].Split(',');

        foreach (var filterVal in filterValsList)
        {
            // remove any single quotes before testing if filter is a num or not
            var cleanFilterVal = filterVal.Replace("'", "").Trim();

            double tempNum = 0;
            if (Double.TryParse(cleanFilterVal, out tempNum))
                newColFilter += string.Format("{0} {1} = {2}", orOperator, colName, cleanFilterVal.Trim());
            else
                newColFilter += string.Format("{0} {1}.Contains('{2}')", orOperator, colName, cleanFilterVal.Trim());

            orOperator = " OR ";
        }

        newColFilter += "))";

        andOperator = " AND ";
    }

    // replace all single quotes with double quotes
    return newColFilter.Replace("'", "\"");
}