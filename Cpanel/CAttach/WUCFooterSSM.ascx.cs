﻿using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_CAttach_WUCFooterSSM : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah);
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah);
            ClassAdmin_Arn.FGetRaeesLagnatAlBahath(DLRaeesLagnatAlBahath);
            ImgModer.ImageUrl = ClassSaddam.FGetSignature_ERP(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
            ImgRaeesLagnatAlBahath.ImageUrl = ClassSaddam.FGetSignature_ERP(Convert.ToInt32(DLRaeesLagnatAlBahath.SelectedValue));
            ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature_ERP(Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue));
            lblModerAlGmeiah.Text = DLModerAlGmeiah.SelectedItem.ToString();
            lblRaeesMaglesAlEdarah.Text = DLRaeesMaglesAlEdarah.SelectedItem.ToString();
            lblRaeesLagnatAlBahath.Text = DLRaeesLagnatAlBahath.SelectedItem.ToString();
            DLModerAlGmeiah.Visible = false;
            DLRaeesMaglesAlEdarah.Visible = false;
            DLRaeesLagnatAlBahath.Visible = false;
            lblModerAlGmeiah.Visible = true;
            lblRaeesMaglesAlEdarah.Visible = true;
            lblRaeesLagnatAlBahath.Visible = true;
        }
    }

    protected void DLModerAlGmeiah_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgModer.ImageUrl = ClassSaddam.FGetSignature_ERP(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
    }

    protected void DLRaeesLagnatAlBahath_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgRaeesLagnatAlBahath.ImageUrl = ClassSaddam.FGetSignature_ERP(Convert.ToInt32(DLRaeesLagnatAlBahath.SelectedValue));
    }

    protected void DLRaeesMaglesAlEdarah_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature_ERP(Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue));
    }

}