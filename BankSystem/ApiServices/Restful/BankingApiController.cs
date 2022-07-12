using Bank.BAL;
using Bank.BAL.DbOperations;
using Bank.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankSystem.ApiServices.Restful
{
    public class BankingApiController : ApiController
    {

        BankRepository repository = null;

        //constructor
        public BankingApiController()
        {
            repository = new BankRepository();
        }

        [HttpPost]
        public IHttpActionResult RegisterUserInApi(UserModel model)
        {
            repository.RegisterUser(model);

            return Ok("success");
        }

        //public IHttpActionResult LoginWithApi(string Email, string Password)
        //{

        //    return Ok();
        //}



        //for login page with api
        [HttpPost]
        public IHttpActionResult LoginUser(LoginModel model)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                if (model.UEmail == "" && model.Password == "")
                {
                    //Dictionary<string, object> success = new Dictionary<string, object>() { { "Message", "empty" } };
                    response.Message = "you need to fill both username and password";
                    response.Status = "empty";
                    return Ok(response);
                }

                UserModel data = repository.UserLogin(model.UEmail, model.Password);
                if (data.UserId != 0)
                {
                    response.Message = "empty";
                    response.Status = "success";
                    response.Data = data;

                    return Ok(response);
                }
                else
                {
                    response.Message = "Wrong UserName & Password";
                    response.Status = "fail";
                    return Ok(response);
                }

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private IHttpActionResult StatusCode(int internalServerError, string message)
        {
            throw new NotImplementedException();
        }


        [HttpPost]
        public IHttpActionResult AddBranchApi(BranchModel model)
        {
            repository.AddBranch(model);

            return Ok("Success");
        }
        //[HttpGet]
        //public IHttpActionResult GetDetailsOfBranch(BranchModel model)
        //{
        //    repository.GetAllBranchData(model);
        //    return Ok();
        //}

        [HttpPost]
        public IHttpActionResult AddAccountApi(AccountModel model)
        {
            repository.addAccount(model);

            return Ok("Success");
        }

        [HttpGet]
        public IHttpActionResult BranchListApi(BranchModel model)
        {
            List<BranchModel> result = repository.GetAllBranchData(model);
            return Ok(result);
        }

        [HttpGet]
        public IHttpActionResult UserListApi()
        {
            List<UserModel> result = repository.UserNameDropDown();
            return Ok(result);
        }

        [HttpGet]
        public IHttpActionResult AccountListApi()
        {


            List<AccountModel> result = repository.GetAccountAllData();
            return Ok(result);
        }


        [HttpGet]
        public IHttpActionResult GetSingleDetailAccountApi(int? id)
        {
            AccountModel result = repository.GetSingleDataById(id);
            return Ok(result);
        }


        //[HttpPut]
        //public IHttpActionResult AccountUpdateApi(int? id,AccountModel model)
        //{
        //    ResponseModel response = new ResponseModel();
        //    repository.UpdateAccountData(model.AccountId, model);
        //    response.Message = "Account Update Successfully";
        //    response.Status = "success";
        //    response.Data = model;

        //    return Ok(response);
        //}

        [HttpPut]
        public IHttpActionResult AccountUpdateApi(AccountModel model)
        {
            ResponseModel response = new ResponseModel();
            repository.UpdateAccountData(model.AccountId, model);
            //response.Message = "Account Update Successfully";
            response.Status = "success";
            //response.Data = model;

            return Ok(response);
        }
        //get single branch data api
        [HttpGet]
        public IHttpActionResult GetSingleDetailBranchApi(int? id)
        {
            BranchModel result = repository.GetSingleDataByBranchId(id);
            return Ok(result);
        }

        [HttpPut]
        public IHttpActionResult BranchUpdateApi(BranchModel model)
        {
            ResponseModel response = new ResponseModel();
            repository.UpdateBranchData(model.BranchId, model);
            //response.Message = "Account Update Successfully";
            response.Status = "success";
            //response.Data = model;
            return Ok(response);
        }
        [HttpPost]
        public IHttpActionResult delBranchApi(BranchModel model)
        {
            ResponseModel response = new ResponseModel();
            repository.DeleteBranchData(model.BranchId);
            response.Message = "Branch Deleted Successfully";
            response.Status = "success";
            response.Data = model;

            return Ok(response);
        }


        [HttpPost]
        public IHttpActionResult delAccountApi(int? id,AccountModel model)
        {
            ResponseModel response = new ResponseModel();
            repository.DeleteAccountData(model.AccountId);
            response.Message = "Account Deleted Successfully";
            response.Status = "success";
            response.Data = model;

            return Ok(response);
        }

        [HttpPost]
        public IHttpActionResult AddTransactionApi(TransactionModel model)
        {
            repository.transaction(model);
            return Ok("Success");

        }

        [HttpGet]
        public IHttpActionResult TransactionListApi()
        {
            List<TransactionModel> result = repository.AllTransactionData();
            return Ok(result);
        }

       

        [HttpPost]
        public IHttpActionResult delAccountApi(AccountModel model)
        {
            ResponseModel response = new ResponseModel();

            repository.DeleteAccountData(model.AccountId);

            response.Message = "Account Deleted Successfully";
            response.Status = "success";
            response.Data = model;

            return Ok(response);
        }




    }

}
