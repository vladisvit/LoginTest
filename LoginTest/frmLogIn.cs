#region Using Statements
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using LWork;
#endregion

namespace LoginTest
{
  /// <summary>
  /// The login form
  /// </summary>
  public partial class frmLogIn: Form
  {

    #region Fields

    // the value of fails
    private int _failedAttempts;

    // the value of permissions on the error at one login procedure 
    private readonly int _attempts;

    // the flag of validate
    private bool _isValid;

    #endregion

    #region Initialization

    public frmLogIn()
    {
      InitializeComponent();
      _failedAttempts = 0;
      _attempts = 3;
      tbxUserName.Validating += new CancelEventHandler(ValidateTextBox);
      tbxPassword.Validating += new CancelEventHandler(ValidateTextBox);
    }

    private void frmLogIn_Load( object sender, EventArgs e )
    {
      LoginWork.Initialization();
    }

    #endregion

    #region Validating

    private void ValidateTextBox( object sender, CancelEventArgs e )
    {
      bool isNameValid = true, isPasswordValid = true;

      if (String.IsNullOrEmpty(((TextBox)sender).Text))
      {
        switch (Convert.ToByte(((TextBox)sender).Tag))
        {
          case 0:
            errorProvider1.SetError(tbxUserName, "Please, enter your name");
            isNameValid = false;
            break;
          case 1:
            errorProvider1.SetError(tbxPassword, "Please, enter your password");
            isPasswordValid = false;
            break;
        }
      }
      else
      {
        switch (Convert.ToByte(((TextBox)sender).Tag))
        {
          case 0:
            errorProvider1.SetError(tbxUserName, "");
            break;
          case 1: errorProvider1.SetError(tbxPassword, "");
            break;
        }
      }
      _isValid = isNameValid && isPasswordValid;
    }

    #endregion

    #region Events Click
    private void btnLogin_Click( object sender, EventArgs e )
    {

      if (_isValid)
      {
        //Check the username and the password
        LoginWork.DoLogin(tbxUserName.Text, tbxPassword.Text);

        if (LoginWork.Logged) //check the logged flag
        {
          this.Close();
        }
        else
        {
          _failedAttempts++;
          if (_failedAttempts > _attempts - 1)
          {
            //You can do to close login form or do waiting user for instance 1 minute
            MessageBox.Show(@"You entered wrong password or username 3 times. \n You can do login after 1 minute");

            Thread.Sleep(1000);
            return;

            //this.Close();
          }

          MessageBox.Show(@"The password or the username are wrong. \n Please, try again. \n Remaining logins: "
            + (_attempts - _failedAttempts).ToString(), "Login failed", MessageBoxButtons.OK);
        }
      }
      else
        MessageBox.Show(@"Please, fill all text boxes");

    }

    private void btnCancel_Click( object sender, EventArgs e )
    {
      this.Close();
    }

    private void linkLabel1_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
    {
      frmRegistration frmReg = new frmRegistration();

      if (DialogResult.OK == frmReg.ShowDialog(this))
      {
        this.tbxUserName.Text = frmReg.tbNikName.Text;
        this.tbxPassword.Text = frmReg.tbPassword.Text;
        ValidateTextBox(tbxUserName, new CancelEventArgs());
        ValidateTextBox(tbxPassword, new CancelEventArgs());

      }

    }

    #endregion


  }
}
