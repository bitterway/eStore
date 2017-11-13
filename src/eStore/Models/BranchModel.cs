using eStore.Models;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
namespace estore.Models
{
    public class BranchModel
    {
        private AppDbContext _db;
        //constructor
        public BranchModel(AppDbContext context)
        {
            _db = context;
        }

        //method to get three closest branches
        public List<Branch> GetThreeClosetBranches(float? lat, float? lng)
        {
            List<Branch> storeDetails = null;
            try
            {
                var latParam = new SqlParameter("@lat", lat);
                var lngParam = new SqlParameter("@lng", lng);
                var query = _db.Branches.FromSql("dbo.pGetThreeClosestBranches @lat = {0}, @lng = {1}", lat, lng);
                storeDetails = query.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return storeDetails;
        }
    }
}