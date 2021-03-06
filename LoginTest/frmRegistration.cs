﻿#region Using Statements

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LWork;

#endregion

namespace LoginTest
{
  /// <summary>
  /// The Registration Form 
  /// </summary>
  public partial class frmRegistration: Form
  {
    // the flag of validate
    private bool _isValid;

    #region Initialization

    public frmRegistration()
    {
      InitializeComponent();
      // The validating events of the text boxes
      tbName.Validating += new CancelEventHandler(ValidateTextBox);
      tbxUserName.Validating += new CancelEventHandler(ValidateTextBox);
      tbPassword.Validating += new CancelEventHandler(ValidateTextBox);
    }

    #endregion

    #region Validating

    /// <summary>
    /// We check all text boxes on the filling data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ValidateTextBox( object sender, CancelEventArgs e )
    {
      bool isNameValid = true, isUserNameValid = true, isPasswordValid = true;
      if (String.IsNullOrEmpty(((TextBox)sender).Text))
      {
        switch (Convert.ToByte(((TextBox)sender).Tag))
        {
          case 0:
            errorProvider1.SetError(tbName, "Please, enter your name");
            isNameValid = false;
            break;
          case 1:
            errorProvider1.SetError(tbxUserName, "Please, enter your nikname");
            isUserNameValid = false;
            break;
          case 2:
            errorProvider1.SetError(tbPassword, "Please, enter your password");
            isPasswordValid = false;
            break;
        }
      }
      else
      {
        switch (Convert.ToByte(((TextBox)sender).Tag))
        {
          case 0: errorProvider1.SetError(tbName, "");
            break;
          case 1: errorProvider1.SetError(tbxUserName, "");
            break;
          case 2: errorProvider1.SetError(tbPassword, "");
            break;
        }
      }

      _isValid = isNameValid && isUserNameValid && isPasswordValid;

    }

    #endregion

    #region Events Click

    private void btnOK_Click( object sender, EventArgs e )
    {

      if (_isValid) //check filling the text boxes
      {
        SaveDataUser();
      }
      else
      {
        MessageBox.Show(this, "You have to fill all text boxes");
        this.DialogResult = DialogResult.Cancel;
      }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Saving the user's registration data 
    /// The registration data presents the structure - UserData
    /// </summary>
    private void SaveDataUser()
    {
      UserData user = new UserData();
      user.Name = tbName.Text;
      user.UserName = tbxUserName.Text;
      user.Password = LoginWork.HashString(tbPassword.Text);

      if (LoginWork.Save(user))
      {
        MessageBox.Show(this, "Data were save");
        this.DialogResult = DialogResult.OK;
      }
      else
      {
        MessageBox.Show(this, "This nik name: " + user.UserName + " is exist. Please, choose another nik name");
        this.DialogResult = DialogResult.Cancel;
      }
    }



    #endregion

  }
}
