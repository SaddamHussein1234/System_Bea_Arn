using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_CAttach_WUCFooterMony : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah);
            ClassAdmin_Arn.FGetAmeenAlsondoq(DLAmeenAlSondoq);
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah);

            ImgModer.ImageUrl = ClassSaddam.FGetSignature_ERP(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
            ImgAmeenAlSondoq.ImageUrl = ClassSaddam.FGetSignature_ERP(Convert.ToInt32(DLAmeenAlSondoq.SelectedValue));
            ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature_ERP(Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue));
            lblModerAlGmeiah.Text = DLModerAlGmeiah.SelectedItem.ToString();
            lblAmeenAlSondoq.Text = DLAmeenAlSondoq.SelectedItem.ToString();
            lblRaeesMaglesAlEdarah.Text = DLRaeesMaglesAlEdarah.SelectedItem.ToString();

            DLModerAlGmeiah.Visible = false;
            DLAmeenAlSondoq.Visible = false;
            DLRaeesMaglesAlEdarah.Visible = false;

            lblModerAlGmeiah.Visible = true;
            lblAmeenAlSondoq.Visible = true;
            lblRaeesMaglesAlEdarah.Visible = true;
        }
    }

    protected void DLModerAlGmeiah_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgModer.ImageUrl = ClassSaddam.FGetSignature_ERP(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
    }

    protected void DLRaeesMaglesAlEdarah_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature_ERP(Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue));
    }

    protected void DLAmeenAlSondoq_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgAmeenAlSondoq.ImageUrl = ClassSaddam.FGetSignature_ERP(Convert.ToInt32(DLAmeenAlSondoq.SelectedValue));
    }

}