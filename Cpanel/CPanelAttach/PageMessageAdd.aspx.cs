using Library_CLS_Arn.ClassEntity.Attach.Models;
using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_CPanelAttach_PageMessageAdd : System.Web.UI.Page
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
                txt_Phone.Focus();
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string XResult = Attach_Repostry_SMS_Send_.FAddSMSMessage(txt_Phone.Text.Trim(), txt_Message.Text.Trim(), DLSenderName.SelectedValue, "Send_SMS", Convert.ToInt32(Test_Saddam.FGetIDUsiq()));
            if (XResult == "IsSuccessAdd")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم إضافة البيانات بنجاح ... ";
            }
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }
    
    protected void RBLFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = false;
            txt_Phone.Text = string.Empty;
            if (RBLFilter.SelectedValue == "Other")
            {
                IDAdmin.Visible = false;
                IDMostafeed.Visible = false;
            }
            else if (RBLFilter.SelectedValue == "Admin")
            {
                IDAdmin.Visible = true;
                IDMostafeed.Visible = false;
                ClassAdmin_Arn.FGetAdminAll(DL_Admin);
            }
            else if (RBLFilter.SelectedValue == "Mostafeed")
            {
                IDAdmin.Visible = false;
                IDMostafeed.Visible = true;
                ClassMosTafeed.FGetNameByPhone(DLMostafeed);
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void DL_Admin_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_Phone.Text = DL_Admin.SelectedValue;
    }

    protected void DLMostafeed_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_Phone.Text = DLMostafeed.SelectedValue;
    }

    protected void LBBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageMessage.aspx");
    }

}