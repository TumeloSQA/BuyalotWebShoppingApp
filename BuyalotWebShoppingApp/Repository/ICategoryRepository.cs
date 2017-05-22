using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuyalotWebShoppingApp.Models;

namespace BuyalotWebShoppingApp.Repository
{
    public interface ICategoryRepository
    {
        List<ProductCategory> findAll();
        ProductCategory find(int id);
    }
}
