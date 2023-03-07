using Library_CLS_Arn.ClassEntity.Attach.Models;
using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_CPanelAttach_PageMessageGroupAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RArabic.Checked = true;

            Attach_Model_SMS_Setting_ AMSS = new Attach_Model_SMS_Setting_();
            AMSS.IDCheck = "GetByID";
            AMSS.ID_Item = new Guid("fc340a50-41ff-4c33-bdd7-4dfa1fdd1752");
            AMSS.Is_Active = true;
            AMSS.Is_Delete = false;
            DataTable dt = new DataTable();
            Attach_Repostry_SMS_Setting_ ARSS = new Attach_Repostry_SMS_Setting_();
            dt = ARSS.BAttach_SMS_Setting_Manage(AMSS);
            if (dt.Rows.Count > 0)
            {
                string XURL = ClassEncryptPassword.Decrypt(dt.Rows[0]["_Url_"].ToString(), CLS_Key.FGetKeyUrl());
                string XUser = ClassEncryptPassword.Decrypt(dt.Rows[0]["_UserName_"].ToString(), CLS_Key.FGetKeyUser());
                string XPass = ClassEncryptPassword.Decrypt(dt.Rows[0]["_Pass_Word_"].ToString(), CLS_Key.FGetKeyPass());

                ClassAPI_SMS api = new ClassAPI_SMS();

                //At Last Bind datatable to dropdown.
                DataTable dtURL = new DataTable();
                dtURL.Columns.Add(new DataColumn("ViewValueURL"));
                dtURL.Rows.Add(XURL);
                DLURL.DataSource = dtURL;
                DLURL.DataTextField = dtURL.Columns["ViewValueURL"].ToString();
                DLURL.DataValueField = dtURL.Columns["ViewValueURL"].ToString();
                DLURL.DataBind();

                DataTable dtSenderName = new DataTable();
                dtSenderName.Columns.Add(new DataColumn("ViewValue"));

                string phrase = api._Get_Sender_Names_(XURL, XUser, XPass);
                string[] words = phrase.Split(',');

                foreach (var word in words)
                {
                    dtSenderName.Rows.Add(word);
                }

                DLSenderName.DataSource = dtSenderName;
                DLSenderName.DataTextField = dtSenderName.Columns["ViewValue"].ToString();
                DLSenderName.DataValueField = dtSenderName.Columns["ViewValue"].ToString();
                DLSenderName.DataBind();
            }
            FGetAlQariah();
            FSelectCheck();
        }
    }

    private void FSelectCheck()
    {
        foreach (ListItem lst in CBAlQariah.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBLMasderAlDakhl.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBFamliyCases.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBAccommodationType.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBHousingStatus.Items) { lst.Selected = true; }
        foreach (ListItem lst in CMMostafeed.Items) { lst.Selected = true; }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (RBLFilter.SelectedValue == "Admin")
            {
                foreach (GridViewRow row in GVAdmin.Rows)
                {
                    if ((row.FindControl("chkSelect") as CheckBox).Checked)
                    {
                        string Phone_ID = Convert.ToString(GVAdmin.DataKeys[row.RowIndex].Value);

                        string XResult = Attach_Repostry_SMS_Send_.FAddSMSMessage(Phone_ID, txt_Message.Text.Trim(), DLSenderName.SelectedValue, "Group_SMS", Convert.ToInt32(Test_Saddam.FGetIDUsiq()));
                        if (XResult == "IsSuccessAdd")
                        {
                            IDMessageWarning.Visible = false;
                            IDMessageSuccess.Visible = true;
                            lblSuccess.Text = "تم إضافة البيانات بنجاح ... ";
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ...";
        }
    }

    protected void LBBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageMessage.aspx");
    }

    protected void RBLFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = false;
            if (RBLFilter.SelectedValue == "Admin")
            {
                IDAdmin.Visible = true;
                IDMostafeed.Visible = false;
                FCheckRPL();
            }
            else if (RBLFilter.SelectedValue == "Mostafeed")
            {
                IDAdmin.Visible = false;
                IDMostafeed.Visible = true;
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void rblCheck_SelectedIndexChanged(object sender, EventArgs e)
    {
        FCheckRPL();
        btnAdd.Focus();
    }

    private void FCheckRPL()
    {
        if (Convert.ToInt32(rblCheck.SelectedValue) == 0)
        {
            FGetAdmin(0, false, false, false);
            txtTitle.Text = "قائمة جميع مستخدمين النظام";
        }
        else if (Convert.ToInt32(rblCheck.SelectedValue) == 1)
        {
            FGetAdmin(1, true, false, false);
            txtTitle.Text = "قائمة أعضاء مجلس الإدارة";
        }
        else if (Convert.ToInt32(rblCheck.SelectedValue) == 2)
        {
            FGetAdmin(2, false, true, false);
            txtTitle.Text = "قائمة الباحثين";
        }
        else if (Convert.ToInt32(rblCheck.SelectedValue) == 3)
        {
            FGetAdmin(3, false, false, false);
            txtTitle.Text = "قائمة المستخدمين";
        }
        else if (Convert.ToInt32(rblCheck.SelectedValue) == 4)
        {
            FGetAdmin(4, false, false, true);
            txtTitle.Text = "قائمة أعضاء الجمعية العمومية";
        }
    }

    public static string FCheckManageF(bool IDP)
    {
        string VaComp = "";
        if (IDP)
            VaComp = "<br /><span style='border-radius:6px; font-size:11px; background-color:Green; color:#FFF; padding: 0 2px 0 2px; margin-left:2px'> مدير النظام </span>";
        return VaComp.ToString();
    }

    public static string FCheckOldMaglisF(bool IDP)
    {
        string VaComp = "";
        if (IDP)
            VaComp = "<br /><span style='border-radius:6px; font-size:11px; background-color:yellowgreen; color:#FFF; padding: 0 2px 0 2px; margin-left:2px'> عضو مجلس قديم </span>";
        return VaComp.ToString();
    }

    public static string FCheckIsBahethF(int ID, string IDUniq, bool IDP)
    {
        string VaComp = "";
        if (IDP)
            VaComp = "<br /><span style='border-radius:6px; font-size:11px; background-color:#0282e2; color:#FFF; padding: 0 2px 0 2px; margin-left:2px'> <a href='PageMultiLinking.aspx?ID=" + ID.ToString() + "&IDUniq=" + IDUniq + "' style='color:#FFF'>ربط القُرى بالباحث</a> </span>";
        return VaComp.ToString();
    }

    public string FCheckGeneral_AssmplyF(string XID, bool XCheck)
    {
        string VaComp = "";
        try
        {
            if (XCheck)
            {
                DataTable dt = new DataTable();
                dt = ClassDataAccess.GetData("SELECT TOP (1) [ID_Admin_Account_],[Is_Delete_] FROM [dbo].[tbl_General_Assmply] With(NoLock) Where [ID_Admin_Account_] = @0 And [Is_Delete_] = @1",
                    XID, Convert.ToString(false));
                if (dt.Rows.Count == 0)
                {
                    VaComp = "<br /><a target='_blank' href='../CPGeneralAssembly/PageGeneralAssemblyAdd.aspx?ID=" +
                        XID + "&IDUniq=" + Convert.ToString(Guid.NewGuid()) + "'><span style='border-radius:6px; font-size:11px;" +
                        " background-color:#0282e2; color:#FFF; padding: 0 2px 0 2px; margin-left:2px'> إضافة ملف العضوية </span></a>";
                }
            }
        }
        catch (Exception)
        {

        }
        return VaComp.ToString();
    }

    private void FGetAdmin(int XID, bool IsAdminInEdarah, bool IsBaheth, bool IsA1)
    {
        ClassAdmin_Arn CAA = new ClassAdmin_Arn();
        CAA._ID_Item = XID;
        CAA._FirstName = string.Empty;
        CAA._IsDelete = false;
        CAA._IsAdminInEdarah = IsAdminInEdarah;
        CAA._IsBaheth = IsBaheth;
        CAA._A1 = IsA1;
        DataTable dt = new DataTable();
        dt = CAA.BArnAdminByAll();
        if (dt.Rows.Count > 0)
        {
            GVAdmin.DataSource = dt;
            GVAdmin.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            btnAdd.Visible = true;
            btnAddMostafeed.Visible = false;
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
            btnAdd.Visible = false;
            btnAddMostafeed.Visible = false;
        }
    }

    protected void LBGetFilter_Click(object sender, EventArgs e)
    {
        pnlHideFilter.Visible = true;
    }

    private void FGetAlQariah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT DISTINCT TOP 1000 AlQaryah , Quaem.AlQriah FROM [dbo].[RasAlEstemarah] With(NoLock)  Inner Join Quaem on Quaem.IDItem = RasAlEstemarah.AlQaryah");
        if (dt.Rows.Count > 0)
        {
            CBAlQariah.DataValueField = "AlQaryah";
            CBAlQariah.DataTextField = "AlQriah";
            CBAlQariah.DataSource = dt;
            CBAlQariah.DataBind();
        }
        FGetAlDakhlAlShahryWaMasdarah();
    }

    private void FGetAlDakhlAlShahryWaMasdarah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where AlDakhlAlShahryWaMasdarah <> @0 And IsDeleteAlDakhlAlShahryWaMasdarah = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            CBLMasderAlDakhl.DataValueField = "IDItem";
            CBLMasderAlDakhl.DataTextField = "AlDakhlAlShahryWaMasdarah";
            CBLMasderAlDakhl.DataSource = dt;
            CBLMasderAlDakhl.DataBind();
        }
        FGetHalafAlMosTafeed();
    }

    private void FGetHalafAlMosTafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where HalatMostafeed <> @0 And IsDeleteHalatMostafeed = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            CBFamliyCases.DataValueField = "IDItem";
            CBFamliyCases.DataTextField = "HalatMostafeed";
            CBFamliyCases.DataSource = dt;
            CBFamliyCases.DataBind();
        }
        FGetAccommodationType();
    }

    private void FGetAccommodationType()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where TypeAlMaskan <> @0 And IsDeleteTypeAlMaskan = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            CBAccommodationType.DataValueField = "IDItem";
            CBAccommodationType.DataTextField = "TypeAlMaskan";
            CBAccommodationType.DataSource = dt;
            CBAccommodationType.DataBind();
        }
        FGetHousingStatus();
    }

    private void FGetHousingStatus()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where HalatAlMaskan <> @0 And IsDeleteHalatAlMaskan = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            CBHousingStatus.DataValueField = "IDItem";
            CBHousingStatus.DataTextField = "HalatAlMaskan";
            CBHousingStatus.DataSource = dt;
            CBHousingStatus.DataBind();
        }
        FGetMostafeed();
    }

    private void FGetMostafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT TOP 1000 RasAlEstemarah.IDItem,(+'( '+CAST(NumberMostafeed as varchar(10)) + ' ) ' + NameMostafeed + ' ( ' + Quaem.AlQriah +' ) ') As 'Name' FROM [dbo].[RasAlEstemarah] With(NoLock) Inner Join Quaem on Quaem.IDItem = RasAlEstemarah.AlQaryah Where TypeMostafeed = @0 And RasAlEstemarah.IsDelete = @1 Order by AlQaryah , NameMostafeed", "دائم", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            CMMostafeed.DataValueField = "IDItem";
            CMMostafeed.DataTextField = "Name";
            CMMostafeed.DataSource = dt;
            CMMostafeed.DataBind();
        }
    }

    protected void btnGetByAlMasder_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = false;
            pnlHideFilter.Visible = false;
            FGetData();
            btnAddMostafeed.Focus();
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "يرجى تحديد قيمة واحدة في القوائم على الاقل ... ";
            return;
        }
    }

    private void FGetData()
    {
        GVMostafeedByDakhl.Columns[0].Visible = true;
        GVMostafeedByDakhl.Columns[16].Visible = true;
        txtSearchByFilter.Text = "SELECT TOP 1000 * FROM [dbo].[RasAlEstemarah] With(NoLock) ";
        txtSearchByFilter.Text += "Where TypeMostafeed = N'" + Convert.ToString(DLTypeMostafeed.SelectedValue) + "' And IsDelete = 0 And ";
        txtSearchByFilter.Text += "(AlDakhlAlShahryllMostafeed between '" + Convert.ToString(txtMasderAlDkhalMinimum.Text.Trim()) + "' And '" + Convert.ToString(txtMasderAlDkhalMaxiMam.Text.Trim()) + "') And ";
        SelectByQriah();
        txtSearchByFilter.Text += " Order By AlQaryah , NameMostafeed";
        GVMostafeedByDakhl.UseAccessibleHeader = false;
        FGetMostafeedByMasderAlDhal();
        System.Threading.Thread.Sleep(500);
    }

    private void SelectByQriah()
    {
        txtSearchByFilter.Text += "(";
        foreach (ListItem lst in CBAlQariah.Items)
        {
            if (lst.Selected == true)
            {

                txtSearchByFilter.Text += " AlQaryah = " + lst.Value;
                txtSearchByFilter.Text += " Or ";
            }
        }
        txtSearchByFilter.Text = txtSearchByFilter.Text.Remove(txtSearchByFilter.Text.Length - 3, 3);
        txtSearchByFilter.Text += ")";
        SelectByMostafeed();
    }

    private void SelectByMostafeed()
    {
        txtSearchByFilter.Text += " And (";
        foreach (ListItem lst in CMMostafeed.Items)
        {
            if (lst.Selected == true)
            {

                txtSearchByFilter.Text += " IDItem = " + lst.Value;
                txtSearchByFilter.Text += " Or ";
            }
        }
        txtSearchByFilter.Text = txtSearchByFilter.Text.Remove(txtSearchByFilter.Text.Length - 3, 3);
        txtSearchByFilter.Text += ")";
        SelectByMasderAlDakhl();
    }

    private void SelectByMasderAlDakhl()
    {
        txtSearchByFilter.Text += " And (";
        foreach (ListItem lst in CBLMasderAlDakhl.Items)
        {
            if (lst.Selected == true)
            {

                txtSearchByFilter.Text += " MasderAlDakhl = " + lst.Value;
                txtSearchByFilter.Text += " Or ";
            }
        }
        txtSearchByFilter.Text = txtSearchByFilter.Text.Remove(txtSearchByFilter.Text.Length - 3, 3);
        txtSearchByFilter.Text += ")";
        SelectByFamliyCases();
    }

    private void SelectByFamliyCases()
    {
        txtSearchByFilter.Text += " And (";
        foreach (ListItem lst in CBFamliyCases.Items)
        {
            if (lst.Selected == true)
            {

                txtSearchByFilter.Text += " HalafAlMosTafeed = " + lst.Value;
                txtSearchByFilter.Text += " Or ";
            }
        }
        txtSearchByFilter.Text = txtSearchByFilter.Text.Remove(txtSearchByFilter.Text.Length - 3, 3);
        txtSearchByFilter.Text += ")";
        SelectByAccommodationType();
    }

    private void SelectByAccommodationType()
    {
        txtSearchByFilter.Text += " And (";
        foreach (ListItem lst in CBAccommodationType.Items)
        {
            if (lst.Selected == true)
            {

                txtSearchByFilter.Text += " TypeAlMasken = " + lst.Value;
                txtSearchByFilter.Text += " Or ";
            }
        }
        txtSearchByFilter.Text = txtSearchByFilter.Text.Remove(txtSearchByFilter.Text.Length - 3, 3);
        txtSearchByFilter.Text += ")";
        SelectByHousingStatus();
    }

    private void SelectByHousingStatus()
    {
        txtSearchByFilter.Text += " And (";
        foreach (ListItem lst in CBHousingStatus.Items)
        {
            if (lst.Selected == true)
            {

                txtSearchByFilter.Text += " HaletAlMasken = " + lst.Value;
                txtSearchByFilter.Text += " Or ";
            }
        }
        txtSearchByFilter.Text = txtSearchByFilter.Text.Remove(txtSearchByFilter.Text.Length - 3, 3);
        txtSearchByFilter.Text += ")";
    }

    private void FGetMostafeedByMasderAlDhal()
    {
        if (CB3.Checked) { GVMostafeedByDakhl.Columns[3].Visible = true; } else { GVMostafeedByDakhl.Columns[3].Visible = false; }
        if (CB4.Checked) { GVMostafeedByDakhl.Columns[4].Visible = true; } else { GVMostafeedByDakhl.Columns[4].Visible = false; }
        if (CB6.Checked) { GVMostafeedByDakhl.Columns[6].Visible = true; } else { GVMostafeedByDakhl.Columns[6].Visible = false; }
        if (CB7.Checked) { GVMostafeedByDakhl.Columns[7].Visible = true; } else { GVMostafeedByDakhl.Columns[7].Visible = false; }
        if (CB8.Checked) { GVMostafeedByDakhl.Columns[8].Visible = true; } else { GVMostafeedByDakhl.Columns[8].Visible = false; }
        if (CB9.Checked) { GVMostafeedByDakhl.Columns[9].Visible = true; } else { GVMostafeedByDakhl.Columns[9].Visible = false; }
        if (CB10.Checked) { GVMostafeedByDakhl.Columns[10].Visible = true; } else { GVMostafeedByDakhl.Columns[10].Visible = false; }
        if (CB11.Checked) { GVMostafeedByDakhl.Columns[11].Visible = true; } else { GVMostafeedByDakhl.Columns[11].Visible = false; }
        if (CB12.Checked) { GVMostafeedByDakhl.Columns[12].Visible = true; } else { GVMostafeedByDakhl.Columns[12].Visible = false; }
        if (CB13.Checked) { GVMostafeedByDakhl.Columns[13].Visible = true; } else { GVMostafeedByDakhl.Columns[13].Visible = false; }
        if (CB14.Checked) { GVMostafeedByDakhl.Columns[14].Visible = true; } else { GVMostafeedByDakhl.Columns[14].Visible = false; }
        if (CB15.Checked) { GVMostafeedByDakhl.Columns[15].Visible = true; } else { GVMostafeedByDakhl.Columns[15].Visible = false; }
        GVMostafeedByDakhl.Columns[0].Visible = true;
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData(Convert.ToString(txtSearchByFilter.Text.Trim()));
        if (dt.Rows.Count > 0)
        {
            GVMostafeedByDakhl.DataSource = dt;
            GVMostafeedByDakhl.DataBind();
            lblCountAll.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlWaiting.Visible = false;
            pnlPrintAllData.Visible = true;
            btnAdd.Visible = false;
            btnAddMostafeed.Visible = true;
        }
        else
        {
            pnlNull.Visible = true;
            pnlWaiting.Visible = false;
            pnlPrintAllData.Visible = false;
            btnAdd.Visible = false;
            btnAddMostafeed.Visible = false;
        }
    }

    protected void btnAddMostafeed_Click(object sender, EventArgs e)
    {
        try
        {
            if (RBLFilter.SelectedValue == "Mostafeed")
            {
                foreach (GridViewRow row in GVMostafeedByDakhl.Rows)
                {
                    if ((row.FindControl("chkSelect") as CheckBox).Checked)
                    {
                        string Phone_ID = Convert.ToString(GVMostafeedByDakhl.DataKeys[row.RowIndex].Value);

                        string XResult = Attach_Repostry_SMS_Send_.FAddSMSMessage(Phone_ID, txt_Message.Text.Trim(), DLSenderName.SelectedValue, "Group_SMS", Convert.ToInt32(Test_Saddam.FGetIDUsiq()));
                        if (XResult == "IsSuccessAdd")
                        {
                            IDMessageWarning.Visible = false;
                            IDMessageSuccess.Visible = true;
                            lblSuccess.Text = "تم إضافة البيانات بنجاح ... ";
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ...";
        }
    }

}