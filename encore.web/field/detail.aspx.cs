﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Encore.TaskManager;
using EntityFramework;
using com.Encore.Web;

public partial class field_detail : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CopyToForm();
        }
    }

    private void CopyToForm()
    {
        // load record
        using (var ctx = new Entities())
        {
            var units = ctx.UNITs.OrderBy<UNIT>("NAME", true);
            ddlUnit.DataSource = units;

            if (Request["ID"] != null)
            {
                var id = -1;
                if (!int.TryParse(Request["ID"], out id))
                {
                    ShowMessage(MessageType.Error, "There was an error loading this record");
                    return;
                }

                var field = ctx.FIELDs.FirstOrDefault(u => u.ID == id);

                if (field != null)
                {
                    btnFormButtons.EntityID = id;
                    txtName.Text = field.NAME;
                    txtDescription.Text = field.DESCRIPTION;
                    ddlUnit.Value = field.UNITID.ToString();
                    btnFormButtons.ShowDelete = false;
                }
            }
        }
    }

    private void PopulateEntity(ref FIELD entity)
    {
        entity.NAME = txtName.Text;
        entity.DESCRIPTION = txtDescription.Text;
        entity.UNITID = short.Parse(ddlUnit.Value);
        entity.MODIFIEDON = DateTime.Now;
    }

    private bool CopyFromForm()
    {
        using (var ctx = new Entities())
        {
            try
            {
                if (btnFormButtons.EntityID.HasValue)
                {
                    var id = btnFormButtons.EntityID;
                    var dataSource = ctx.FIELDs.FirstOrDefault(u => u.ID == id);
                    PopulateEntity(ref dataSource);
                    Audit.Log(ctx, AuditType.Edit, this.Page.GetType().FullName,
                              string.Format("Field Edited. ID: {0}", dataSource.ID), LoggedInUser.ID);
                }
                else
                {
                    var dataSource = new FIELD {CREATEDON = DateTime.Now};
                    PopulateEntity(ref dataSource);
                    ctx.AddToFIELDs(dataSource);
                    Audit.Log(ctx, AuditType.Add, this.Page.GetType().FullName,
                              string.Format("Field Added. Name: {0}", txtName.Text), LoggedInUser.ID);
                }

                ctx.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                ExceptionLog.WriteException("Save Field", ex);
                ShowMessage(MessageType.Error, "There was an error saving this record");
            }
        }
        return false;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (CopyFromForm())
            {
                Response.Redirect("default.aspx?saved=t");
            }
        }
    }
}