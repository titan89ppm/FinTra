using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace startUpForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            initialFilePathCheck();
        }

        startUp startup = new startUp();

        /// <summary>
        /// checks for available user and checks that the password is correct
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string Username = tbUsername.Text;
            string[] passwords = { tbPassword.Text, tbConfirmPassword.Text };
            if (isUser(Username))
            {
                if(checkPasswordCorrect(Username, passwords))
                {
                    MessageBox.Show("Thank you for logging in");
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show("Please try typing in your passwords again");
                }
            }
            else
            {
                MessageBox.Show("Sorry this username doesn't exist, please try again");
            }

        }

        /// <summary>
        /// When create account button is clicked, the program checks that the username doesn't already exist.  If it does exist
        /// the program won't continue.  Otherwise the folder is created and the credentials are saved in the folder
        /// the password will also be checked for a match other wise the program wil not continue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            if (checkBoxFill())
            {
                string Username = tbUsername.Text;
                string[] passwords = { tbPassword.Text, tbConfirmPassword.Text };
                if (checkPasswordMatch(passwords))
                {
                    if (checkUsername(Username))
                    {
                        createUser(Username);
                        Application.Exit();
                    }
                    else
                    {
                        MessageBox.Show("Sorry this username already exists");
                    }

                }
                else
                {
                    MessageBox.Show("Sorry These Passwords don't match");
                }
            }
            else
            {
                MessageBox.Show("Sorry you haven't filled in all the boxes");
            }         
            
        }



        /// <summary>
        /// Checks to see if the mandatory FinTra files have already been set up, if this is a new computer there will not be a directory
        /// for the Users to be stored in (along with all other information)
        /// This is called when form is opened
        /// </summary>
        private void initialFilePathCheck()
        {
            if (!File.Exists("C:\\FinTra"))
            {
                Directory.CreateDirectory("C:\\FinTra");
            }
            if (!File.Exists("C:\\FinTra\\Users"))
            {
                Directory.CreateDirectory("C:\\FinTra\\Users");
            }
        }


        /// <summary>
        /// Function to check whether the username has already been taken
        /// </summary>
        /// <param name="Username"></param>
        private bool checkUsername(string Username)
        {
            string routePath = "C:\\FinTra\\Users\\";
            string userPath = Path.Combine(routePath, Username);
            if (Directory.Exists(userPath))
            {
                tbUsername.Clear();
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// Function creates a new user
        /// </summary>
        /// <param name="Username"></param>
        private void createUser(string Username)
        {
            string routePath = "C:\\FinTra\\Users\\";        
            string userPath = Path.Combine(routePath, Username);
            Directory.CreateDirectory(userPath);
            string[] credentials = { tbUsername.Text, tbPassword.Text };
            string CredentialFormat = "- credentials.txt";
            string userCredentials = CredentialFormat.Insert(0, Username);
            userPath = Path.Combine(userPath, userCredentials);
            File.WriteAllLines(userPath, credentials);
        }


        /// <summary>
        /// function checks for user login
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        private bool isUser(string Username)
        {
            string routePath = "C:\\FinTra\\Users\\";
            string userPath = Path.Combine(routePath, Username);
            if(Directory.Exists(userPath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// function checks if the passwords match
        /// </summary>
        /// <param name="passwords"></param>
        /// <returns></returns>
        private bool checkPasswordMatch(string[] passwords)
        {
            if(passwords[0] != passwords[1])
            {
                tbConfirmPassword.Clear();
                tbPassword.Clear();
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// checks the password against the requested username is correct
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Passwords"></param>
        /// <returns></returns>
        private bool checkPasswordCorrect(string Username, string[] Passwords)
        {
            if(checkPasswordMatch(Passwords))
            {
                StreamReader file = new StreamReader(getUserCredentialPath(Username));
                string u = file.ReadLine();
                string p = file.ReadLine();

                if(p != Passwords[0])
                {
                    return false;
                }
                else
                {
                    return true;
                }
                
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// returns file pakth for user credentials
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        private string getUserCredentialPath(string Username)
        {
            string routePath = "C:\\FinTra\\Users\\";
            string userPath = Path.Combine(routePath, Username);            
            string CredentialFormat = "- credentials.txt";
            string userCredentials = CredentialFormat.Insert(0, Username);
            userPath = Path.Combine(userPath, userCredentials);

            return userPath;
            
        }

        /// <summary>
        /// checks all boxes have been filled
        /// </summary>
        /// <returns></returns>
        private bool checkBoxFill()
        {
            if ((tbConfirmPassword.Text != "") && (tbPassword.Text != "") && (tbUsername.Text != ""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
      


    }
}
