using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_test : System.Web.UI.Page
{
    string script = "<script type = 'text/javascript'>alert('Hello');</script>";
    List<CurrencyInfo> currencies = new List<CurrencyInfo>();
    protected void Page_Load(object sender, EventArgs e)
    {
        decimal x = 32131321;
        lblDecamal.Text = x.ToString();
        BArnGetData();

        currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Syria));
        currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.UAE));
        currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
        currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Tunisia));
        currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Gold));

        cboCurrency.DataSource = currencies;

        //cboCurrency_DropDownClosed(null, null);

        lblDecrypt.Text = ClassEncryptPassword.Decrypt("PxXlnPWXKKEFvzKpYOpgOG4pTo6CEv7Lpo6srOXdkY/uF09tRXB31OFNvC4noDA9CQWXLvbIFmWFBrpCnMVTdnyi5ZFXZQLHVj9F3Ru27KGwHK+H1cBL7Tb/fcfWEi2/", "لا اله الا انت سبحانك اني كنت من الظالمين");
    }

    protected void btnAlert_Click(object sender, EventArgs e)
    {
        string message = "Hello! Mudassar.";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.onload=function(){");
        sb.Append("alert('");
        sb.Append(message);
        sb.Append("')};");
        sb.Append("</script>");
        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
    }



    protected void btnClick_Click(object sender, EventArgs e)
    {
        string word = ConvertNumbertoWords(Convert.ToInt32(txtnumberw.Text));
        lblmsg.InnerText = word;
    }

    public static string ConvertNumbertoWords(int number)
    {
        if (number == 0)
            return "صفر";
        if (number < 0)
            return "minus " + ConvertNumbertoWords(Math.Abs(number));
        string words = "";
        if ((number / 1000000) > 0)
        {
            words += ConvertNumbertoWords(number / 1000000) + " مليون ";
            number %= 1000000;
        }
        if ((number / 1000) > 0)
        {
            words += ConvertNumbertoWords(number / 1000) + " ألف ";
            number %= 1000;
        }
        if ((number / 100) > 0)
        {
            words += ConvertNumbertoWords(number / 100) + "مائة";
            number %= 100;
        }
        if (number > 0)
        {
            if (words != "")
                words += "و ";
            //var unitsMap = new[] { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
            var unitsMap = new[] { "صفر", "واحد", "اثنان", "ثلاثه", "أربعه", "خمسه", "سته", "سبعه", "ثمانيه", "ثمانيه", "تسعه", "عشره", "اثنا عشر", " ثلاثه عشر ", " أربعه عشر ", " خمسه عشر ", " سته عشر ", " سبعه عشر ", " ثمانيه عشر ", " تسعه عشر " };
            var tensMap = new[] { "صفر", "عشرة", "عشرون", "ثلاثون", "أربعون", "خمسون", "ستون", "سبعون", "ثمانون", "تسعون" };
            //var tensMap = new[] { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += " " + unitsMap[number % 10];
            }
        }
        return words;
    }
    
    protected void txtNumber_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ToWord toWord = new ToWord(Convert.ToDecimal(txtNumber.Text), currencies[Convert.ToInt32(2)]);
            txtEnglishWord.Text = toWord.ConvertToEnglish();
            txtArabicWord.Text = toWord.ConvertToArabic();
        }
        catch (Exception ex)
        {
            txtEnglishWord.Text = String.Empty;
            txtArabicWord.Text = String.Empty;
        }
    }

    public static DataTable GetData(string sql, string Name)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(_Link_Connection_.ConnectionString_System_))
        {

            
            SqlDataAdapter adapt = new SqlDataAdapter(sql, con);
            adapt.SelectCommand.Parameters.AddWithValue("@0", Name);
            con.Open();
            adapt.Fill(dt);
            con.Close();

        }
        return dt;
    }

    public void BArnGetData()
    {
        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = AddParamater("@0", 507, SqlDbType.Int, 50);
        parameters[1] = AddParamater("@1", "دائم", SqlDbType.NVarChar, 50);
        DataTable dt = new DataTable();
        dt = ExecuteDTBySelect("Select * from RasAlEstemarah Where NumberMostafeed = @0 And TypeMostafeed = @1", parameters);
        if (dt.Rows.Count > 0)
        {
            RPTMostafeed.DataSource = dt;
            RPTMostafeed.DataBind();
        }
    }

    public static SqlParameter AddParamater(string parameterName, object value, SqlDbType DbType, int size)
    {
        SqlParameter param = new SqlParameter();
        param.ParameterName = parameterName;
        param.Value = value.ToString();
        param.Size = size;
        param.Direction = ParameterDirection.Input;
        return param;
    }

    public static DataTable ExecuteDTBySelect(string ProcedureName, SqlParameter[] Params)
    {
        SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = ProcedureName;
        cmd.Parameters.AddRange(Params);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter adopter = new SqlDataAdapter(cmd);
        DataTable dTable = new DataTable();

        try
        {
            adopter.Fill(dTable);
        }
        catch (Exception)
        {

        }

        finally
        {
            //Disposing
            adopter.Dispose();
            cmd.Parameters.Clear();
            cmd.Dispose();
            conn.Dispose();
        }
        return dTable;
    }

}