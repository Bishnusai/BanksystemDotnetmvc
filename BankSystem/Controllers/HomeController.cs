using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bank.BAL;
using Bank.DAL;
using log4net;
using Bank.BAL.DbOperations;
using System.Web.Security;

namespace BankSystem.Controllers
{
    public class HomeController : Controller
    {

        //instance of BankRepository where declared logic of the bank system controller
        BankRepository repository = null;

        //constructor
        public HomeController()
        {
            repository = new BankRepository();
        }

        //main page of the bank system project
        public ActionResult Index()
        {
            return View();
        }


        //For User Registration
        // GET: Home
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(UserModel model)
        {

            if (ModelState.IsValid)
            {
                int id = repository.RegisterUser(model);
                if (id > 0)
                {
                    ModelState.Clear();
                    ViewBag.Message = "User Registered Successfully";
                }
            }
           
            return RedirectToAction("Login");
        }


      


        //for login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {

            if (ModelState.IsValid)
            {
                string data = repository.IsValidUser(model.UEmail, model.Password);
                if (data != null)
                {
                    FormsAuthentication.SetAuthCookie(model.UEmail, false);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Failure", "Username and password not matched!");
                    return View();
                }
            }
            else
            {
                return View(model);
            }
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Login");
        }

        //For Register Branch
        public ActionResult RegisterBranch()
        {
            return View();
        }


        [HttpPost]
        public ActionResult RegisterBranch(BranchModel model)
        {
            if (ModelState.IsValid)
            {
                int id = repository.AddBranch(model);
                if (id > 0)
                {
                    ModelState.Clear();
                    ViewBag.Message = "Branch Added Successfully";
                }
                
            }
            return RedirectToAction("LoginIndex");
        }


        //  list of branch records
        [HttpGet]
        public ActionResult GetAllBranchRecords(BranchModel model)
        {
            var GetBranchRecord = repository.GetAllBranchData(model);
            return View(GetBranchRecord);
        }
        //Get Single branch Details
        public ActionResult GetSingleBranchDetails(int id)
        {
            var GetbranchRecord = repository.GetSingleDataByBranchId(id);
            return View(GetbranchRecord);
        }


        //Edit of Account Data
        public ActionResult Editbranch(int id)
        {
            var EditBranch = repository.GetSingleDataByBranchId(id);
            return View(EditBranch);
        }
        [HttpPost]
        public ActionResult Editbranch(BranchModel model)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateBranchData(model.BranchId, model);
                return RedirectToAction("GetDetailsOfAccount");
            }
            return View();
        }

        //For Add Account model and bind with user data with their id and firstname and show 
        //them in dropdown list manner with branch dropdown
        [HttpGet]
        public ActionResult LoginIndex()
        {
            AccountModel model = new AccountModel();

            model.UserList = repository.UserNameDropDown();
            model.BranchList = repository.BranchDropDown();

            return View(model);
        }
        [HttpPost]
        public ActionResult LoginIndex(AccountModel model)
        {
            AccountModel data = repository.addAccount(model);

            if (ModelState.IsValid)
            {
                ViewBag.isSuceess = "data and id added.....!!";
            }

            //return View();
            //AccountModel model = new AccountModel();
            //return method by post method
            model.UserList = repository.UserNameDropDown();
            model.BranchList = repository.BranchDropDown();
            return View(model);
        }

        //Account Dropdown method
        [HttpGet]
        public ActionResult DisplayAccountNuInDropdown()
        {
            AccountModel model = new AccountModel();
            model.AccountList = repository.AccountDropDown();
            return View(model);

        }
        [HttpGet]
        //list of Account Records
        public ActionResult GetDetailsOfAccount()
        {
            var AccountsData = repository.GetAccountAllData();
            return View(AccountsData);
        }





        //Get Single Account Details
        public ActionResult GetSingleAccountDetails(int id)
        {
            var GetUserRecord = repository.GetSingleDataById(id);
            return View(GetUserRecord);
        }


        //Edit of Account Data
        public ActionResult Edit(int id)
        {
            var EditEmp = repository.GetSingleDataById(id);
            return View(EditEmp);
        }
        [HttpPost]
        public ActionResult Edit(AccountModel model)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateAccountData(model.AccountId, model);
                return RedirectToAction("GetDetailsOfAccount");
            }
            return View();
         }


        //Cash Transactions
        [HttpGet]
        public ActionResult TransactionSystem()
        {
           
            TransactionModel model = new TransactionModel();
            model.AccountList= repository.AccountDropDown();
            //model.UserList = repository.UserNameDropDown();
 
            return View(model);
        }

        [HttpPost]
        public ActionResult TransactionSystem(TransactionModel model)
        {

            if (ModelState.IsValid)
            {
                model.CashTransaction.Count = CashNoteCount(
                 model.CashTransaction.C100,
                 model.CashTransaction.C200,
                 model.CashTransaction.C500,
                 model.CashTransaction.C2000
                 );
                bool compareAmount = CashNoteCount(
                   model.CashTransaction.C100,
                   model.CashTransaction.C200,
                   model.CashTransaction.C500,
                   model.CashTransaction.C2000,
                   model.TransactionAmount
                   );

                if (compareAmount == true)
                {
                    model.Account = repository.GetSingleDataById(model.AccountId);
                    

                    if (model.TransactionAmount >= model.Account.WithdrwaLimit)
                    {
                        ViewBag.limitError = "You Can Not Withdrawl This Much Money";
                        model.AccountList = repository.AccountDropDown();
                        return View(model);
                    }

                    if (model.TransactionAmount > model.Account.MinimumBalance)
                    {
                        ViewBag.minBalamceErr = "Warnng.. Your Account Balnance is Very Less";
                        model.AccountList = repository.AccountDropDown();
                        return View(model);
                    }

                    TransactionModel data = repository.transaction(model);
                    //model.AccountList = repository.AccountDropDown();
                    return RedirectToAction("ShowAllTransactData");
                }
                
                //model.UserList = repository.UserNameDropDown();
                else
                {
                    ViewBag.message = "Your Amount Does Not Matches With The Note Count";
                    model.AccountList = repository.AccountDropDown();

                }
            }
           
                return View(model); 
        }
        [NonAction]
        public int CashNoteCount(int C100, int C200, int C500, int C2000)
        {
            return C100 + C200 + C500 + C2000;
        }

        [NonAction]
        public bool CashNoteCount(int C100, int C200, int C500, int C2000, decimal trancsactionamount)
        {
            int TotalAmount = ((C100 * 100) + (C200* 200) +(C500 * 500) + (C2000 * 2000));
            
            if (TotalAmount == trancsactionamount)
            {
                return true;
            }
            return false;
        }


        //Show all Transaction Data
        [HttpGet]
        public ActionResult ShowAllTransactData()
        {
            var GetTransactionReport = repository.AllTransactionData();
           return View(GetTransactionReport);
        }
        [HttpGet]
        public ActionResult DeleteBranch(int? id)
        {
            repository.DeleteBranchData(id);
            return RedirectToAction("GetAllBranchRecords");

        }

        //Delete Account data
        [HttpGet]
            public ActionResult Delete(int? id)
            {
                  repository.DeleteAccountData(id);
                  return RedirectToAction("GetDetailsOfAccount");

            }
    }
}