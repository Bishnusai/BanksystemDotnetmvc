using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.DAL;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Bank.BAL.DbOperations
{
   public class BankRepository
    {
        BankSystemEntities db = new BankSystemEntities();

        //Register user
        public int RegisterUser(UserModel model)
        {
            User UserData = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UEmail = model.UEmail,
                UMobileNumber = model.UMobileNumber,
                UAddress = model.UAddress,
                Password = model.Password
            };
           
            db.User.Add(UserData);
            db.SaveChanges();
            return UserData.UserId; 
            
        }



        //list of AllUserData
        //public List<UserModel> GetAllUserData()
        //{
        //    List<UserModel> ReturnUserData = db.User.Select(x => new UserModel
        //    {
        //        UserId=x.UserId,
        //        FirstName=x.FirstName,
        //        LastName=x.LastName,
        //        UEmail=x.UEmail,
        //        UMobileNumber=x.UMobileNumber,
        //        UAddress=x.UAddress

        //    }).ToList();

        //    return ReturnUserData;
        //}
        //public UserModel GetSingleData(int? id)
        //{

        //    UserModel GetUserSingleData = db.User.Where(x => x.UserId == id).Select(x => new UserModel()
        //    {
        //        UserId = x.UserId,
        //        FirstName = x.FirstName,
        //        LastName = x.LastName,
        //        UEmail = x.UEmail,
        //        UMobileNumber = x.UMobileNumber,
        //        UAddress = x.UAddress

        //    }).FirstOrDefault();
        //    return GetUserSingleData;

        //}

        //for login page and validate the user data with email and password
        public string IsValidUser(string email, string password)
        {
            if (db.User.Any(x => x.UEmail == email && x.Password == password))
            {
                return email;
            }
            throw new UnauthorizedAccessException("Invalid");
        }

    
        public UserModel UserLogin(string email, string password)
        {
            var objUser = db.User.Where(x => x.UEmail == email && x.Password == password).FirstOrDefault();

            UserModel objUserModel = new UserModel();

            if (objUser != null)
            {
                objUserModel.UserId = objUser.UserId;
                objUserModel.UEmail = objUser.UEmail;
                objUserModel.FirstName = objUser.FirstName;
                objUserModel.LastName = objUser.LastName;
                objUserModel.UMobileNumber = objUser.UMobileNumber;
                objUserModel.UAddress = objUser.UAddress;

            }

            return objUserModel;
            //throw new UnauthorizedAccessException("Invalid User Details");
            //return email;
        }



        //Adding branch data
        public int AddBranch(BranchModel model)
        {
            Branch branchData = new Branch
            {
                BranchName = model.BranchName,
                BranchNumber = model.BranchNumber
            };
            db.Branch.Add(branchData);
            db.SaveChanges();
            return branchData.BranchId;
        }

        

        //showing branch data in list manner 
        public List<BranchModel> GetAllBranchData(BranchModel model)
        {

            List<BranchModel> GetBranchData = db.Branch.Select(x => new BranchModel()
            {
                BranchId = x.BranchId,
                BranchName = x.BranchName,
                BranchNumber = x.BranchNumber,

            }).ToList();
            return GetBranchData;

        }

        //get branch data by id
        public BranchModel GetSingleDataByBranchId(int? id)
        {
            var result = db.Branch.Where(x => x.BranchId == id).Select(x => new BranchModel()
                {
                    BranchId=x.BranchId,
                    BranchName= x.BranchName,
                    BranchNumber = x.BranchNumber                
                }).FirstOrDefault();
            return result;
        }
        //update branchData
        public bool UpdateBranchData(int? id, BranchModel model)
        {

            Branch Getbranch = db.Branch.FirstOrDefault(x => x.BranchId == id);

            if (Getbranch != null)
            {
                Getbranch.BranchId = model.BranchId;
                Getbranch.BranchName = model.BranchName;
                Getbranch.BranchNumber = model.BranchNumber;
            }
            db.SaveChanges();
            return true;
        }


        public List<TransactionModel> AllTransactionData()
        {
            //List<Transaction> GetTransactData1 = db.Transaction.ToList();
            //act_id = db.Transaction.Where(x => x.AccountId == id).

            
            List <TransactionModel> GetTransactData = db.Transaction.Select(x => new TransactionModel()
            {
                TransactionId = x.TransactionId,
                AccountId = ( x.AccountId)??0,
                TransactionAmount=x.TransactionAmount,
                transactionType=x.TransactionType,
                TransactionDate=x.TransactionDate,

                Account = new AccountModel
                {

                    AccountNumber = x.Account.AccountNumber,
                    AccountBalance = x.Account.AccountBalance
                }


            }).ToList();

            return GetTransactData;
        }



        //adding account model with account table mapping with user data
        public AccountModel addAccount(AccountModel model)
        {
               //User data = new User();
               //Branch data2 = new Branch();
            Account table = new Account()
            {
                UserId = model.UserId,
                BranchId = model.BranchId,
                AccountNumber = model.AccountNumber,
                MinimumBalance = model.MinimumBalance,
                WithdrwaLimit = model.WithdrwaLimit,
                AccountBalance = model.AccountBalance
            };

            db.Account.Add(table);
            db.SaveChanges();

            return model;
        }

        //dropdown for user with account binding
        public List<UserModel> UserNameDropDown()
        {
            List<UserModel> result = new List<UserModel>();

            var list = db.User.Select(m => m).ToList();

            if (list != null && list.Count() > 0)
            {
                foreach (var data in list)
                {
                    UserModel model = new UserModel()
                    {
                        UserId = data.UserId,
                        FirstName = data.FirstName,
                        LastName = data.LastName,
                        UEmail = data.UEmail,
                        UMobileNumber = data.UMobileNumber,
                        UAddress = data.UAddress
                    };
                    result.Add(model);
                }
            }
            return result;
        }

        //for branch dropdown list bind with account
        public List<BranchModel> BranchDropDown()
        {
            List<BranchModel> result1 = new List<BranchModel>();
            var list = db.Branch.Select(m => m).ToList();
            if (list != null && list.Count() > 0)
            {
                foreach (var data2 in list)
                {
                    BranchModel model = new BranchModel()
                    {
                        BranchId = data2.BranchId,
                        BranchName = data2.BranchName,
                        BranchNumber = data2.BranchNumber
                    };
                    result1.Add(model);
                }
            }
            return result1;
        }


        //dropdown for account model
        public List<AccountModel> AccountDropDown()
        {

            List<AccountModel> result = new List<AccountModel>();
            var list = db.Account.Select(m => m).ToList();
            if (list != null && list.Count() > 0)
            {
                foreach (var data in list)
                {
                    AccountModel model = new AccountModel()
                    {
                        AccountId = data.AccountId,
                        AccountNumber = data.AccountNumber,
                        AccountBalance = data.AccountBalance

                    };
                    result.Add(model);
                }
            }
            return result;
        }


        public TransactionModel transaction(TransactionModel model)
        {

            Account account = db.Account.Where(x => x.AccountId == model.AccountId).FirstOrDefault();
            if (account != null)
            {
                model.UserId = account.User.UserId;
            }

            Transaction table = new Transaction()
            {
                //TransactionId=model.TransactionId,
                AccountId = model.AccountId,                
                TransactionAmount = model.TransactionAmount,
                //check null or not
                TransactionType = !(string.IsNullOrEmpty(model.transactionType)) ? model.transactionType:"",
                TransactionDate = DateTime.Now,
                UserId = account.User.UserId
        };

            table.CashTransactions.Add(new CashTransactions()
            {
                C100 = model.CashTransaction.C100,
                C200 = model.CashTransaction.C200,
                C500 = model.CashTransaction.C500,
                C2000 = model.CashTransaction.C2000,
                Transaction = table,
                Cash = (table.TransactionAmount),
                Count = model.CashTransaction.Count
            });

            account.AccountBalance = model.transactionType == "D" ? (account.AccountBalance + model.TransactionAmount) : (account.AccountBalance - model.TransactionAmount);
            //Account obj = new Account()
            //{
            //    AccountBalance = model.transactionType == "D" ? (model.Account.AccountBalance + model.TransactionAmount) : (model.Account.AccountBalance - model.TransactionAmount),
            //    AccountId = model.Account.AccountId,
            //    BranchId = model.Account.BranchId,
            //    UserId = model.Account.UserId,
            //    AccountNumber = model.Account.AccountNumber,
            //    MinimumBalance = model.Account.MinimumBalance,
            //    WithdrwaLimit = model.Account.WithdrwaLimit
            //};

            db.Transaction.Add(table);
            
            //db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();

            return model;

        }




        //list of Account 
        public List<AccountModel> GetAccountAllData()
        {
            List<AccountModel> AccountData = db.Account.Select(x => new AccountModel()
            {
                
                AccountId=x.AccountId,
                AccountNumber=x.AccountNumber,              
                MinimumBalance=x.MinimumBalance,
                AccountBalance = x.AccountBalance,
                WithdrwaLimit = x.WithdrwaLimit,
                UserId = x.UserId,
                BranchId = x.BranchId,
                User = new UserModel()
                {
                    FirstName = x.User.FirstName
                },
                Branch=new BranchModel()
                {
                    BranchName=x.Branch.BranchName
                }


            }).ToList();
            return AccountData;
        }


        //Get Sigle data of Account table
        //public AccountModel GetSingleDataById(int? id)
        //{

        //    AccountModel GetUserSingleData = db.Account.Where(x => x.AccountId == id).Select(x => new AccountModel()
        //    {
        //       AccountId=x.AccountId,
        //       AccountNumber=x.AccountNumber,
        //       AccountBalance=x.AccountBalance,
        //        MinimumBalance = x.MinimumBalance,
        //        WithdrwaLimit=x.WithdrwaLimit,
        //        UserId = x.UserId,
        //        BranchId = x.BranchId,
        //        User=new UserModel()
        //        {
        //            UserId = x.User.UserId,

        //            FirstName=x.User.FirstName
        //        },
        //        Branch = new BranchModel()
        //        {
        //            BranchId = x.Branch.BranchId,
        //            BranchName = x.Branch.BranchName
        //        }


        //    }).FirstOrDefault();

        //    return GetUserSingleData;

        //}


        public AccountModel GetSingleDataById(int? id)
        {
            var result = db.Account
                .Where(x => x.AccountId == id).Select(x => new AccountModel()
                {
                    AccountId = x.AccountId,
                    AccountNumber = x.AccountNumber,
                    MinimumBalance = x.MinimumBalance,
                    WithdrwaLimit = x.WithdrwaLimit,
                    AccountBalance = x.AccountBalance,
                    UserId = x.UserId,
                    BranchId = x.BranchId,
                    Branch = db.Branch.Where(b => b.BranchId == x.BranchId).Select(br => new BranchModel()
                    {
                        BranchId = br.BranchId,
                        BranchName = br.BranchName,
                        BranchNumber = br.BranchNumber }).FirstOrDefault(),
                    User = db.User.Where(u => u.UserId == x.UserId).Select(ur => new UserModel()
                    {
                        UserId = ur.UserId,
                        FirstName = ur.FirstName,
                        UAddress = ur.UAddress,
                        UEmail = ur.UEmail,
                        UMobileNumber = ur.UMobileNumber,
                        LastName = ur.LastName }).FirstOrDefault(),

                }).FirstOrDefault();
            return result;
        }


        //Edit/Update Account data
        public bool UpdateAccountData(int? id, AccountModel model)
        {

            Account GetUser = db.Account.FirstOrDefault(x => x.AccountId == id);

            if (GetUser != null)
            {
                GetUser.AccountId = model.AccountId;
                GetUser.AccountNumber = model.AccountNumber;
                GetUser.AccountBalance = model.AccountBalance;
                GetUser.MinimumBalance = model.MinimumBalance;
                GetUser.WithdrwaLimit = model.WithdrwaLimit;
               
            }
           
             db.SaveChanges();
            return true;

        }


        //Delete AccountData
        public bool DeleteBranchData(int? id)
        {
            var act_id = db.Account.Where(x => x.BranchId == id).FirstOrDefault();
            //var tra_id = db.Transaction.Where(x => x.AccountId == act_id.AccountId).FirstOrDefault();
            //var cash_id = db.CashTransactions.Where(x => x.TransactionId == tra_id.TransactionId).FirstOrDefault();

            List<Account> delAccount = db.Account.Where(x => x.BranchId== id).ToList();
            db.Account.RemoveRange(delAccount);
            db.SaveChanges();
            Branch DelBranch = db.Branch.FirstOrDefault(x => x.BranchId == id);

            if (DelBranch != null)
            {
                db.Branch.Remove(DelBranch);
                db.SaveChanges();
                return true;

            }
            return false;

        }



        //Delete AccountData
        public bool DeleteAccountData(int? id)
        {

            Account DelAccount = db.Account.FirstOrDefault(x => x.AccountId == id);

            //remove foreign key which is related to the table before delete main data otherwise it will throw error.
            List<CashTransactions> lstCashTransaction = db.CashTransactions.Where(x => x.Transaction.AccountId == id).ToList();
            db.CashTransactions.RemoveRange(lstCashTransaction);
            db.SaveChanges();

            //remove foreign key which is related to the table before delete main data otherwise it will throw error.
            List<Transaction> lstTransaction = db.Transaction.Where(x => x.AccountId == id).ToList();
            db.Transaction.RemoveRange(lstTransaction);
            db.SaveChanges();
            
            if (DelAccount != null)
            {
                db.Account.Remove(DelAccount);
                db.SaveChanges();
                return true;
                
            }
            return false;

        }

    }
}