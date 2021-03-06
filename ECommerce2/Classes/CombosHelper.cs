﻿using ECommerce2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace ECommerce2.Classes
{
    public class CombosHelper : IDisposable
    {
        private static ECommerceContext db = new ECommerceContext();

        public static List<State> GetStates()
        {
            var states = db.States.ToList();

            states.Add(new State
            {
                StateId = 0,
                Name = "[Select a department]..."
            });

            return states.OrderBy(d => d.Name).ToList();

        }

        public static List<Product> GetProducts(int companyId, bool sw)
        {
            var products = db.Products.Where(p => p.CompanyId == companyId).ToList();
            return products.OrderBy(p => p.Description).ToList();
        }


        public static List<Product> GetProducts(int companyId)
        {
            var product = db.Products.Where(p => p.CompanyId ==companyId).ToList();
            product.Add(new Product
            {
                ProductId = 0,
                Description="[Select a product ...]"
            });
            return product.OrderBy(p => p.Description).ToList();
        }

        public static List<Product> GetProducts()
        {
            var product = db.Products.ToList();
            product.Add(new Product
            {
                ProductId = 0,
                Description = "[Select a product ...]"
            });
            return product.OrderBy(p => p.Description).ToList();
        }

        public static List<City> GetCities()
        {
            var cities = db.Cities.ToList();
            cities.Add(new City
            {
                CityId = 0,
                Name = "[Select a city...]"
            });

            return cities.OrderBy(d => d.Name).ToList();
        }

        public static List<City> GetCities(int stateId)
        {
            var cities = db.Cities.Where(c => c.StateId == stateId).OrderBy(d => d.Name).ToList();
            cities.Add(new City
            {
                CityId = 0,
                Name = "[Select a City...]"
            });

            return cities.OrderBy(d => d.Name).ToList();
        }

        public static List<Company> GetCompanies()
        {
            var companies = db.Companies.ToList();
            companies.Add(new Company
            {
                CityId = 0,
                Name = "[Select a company...]"
            });

            return companies.OrderBy(d => d.Name).ToList();
        }

        public static List<Customer> GetCustomers(int companyId)
        {
            var qry = (from cu in db.Customers
                       join cc in db.CompanyCustomers on cu.CustomerId equals cc.CustomerId
                       join co in db.Companies on cc.CompanyId equals co.CompanyId
                       where co.CompanyId == companyId
                       select new { cu }).ToList();

            var customers = new List<Customer>();
            foreach (var item in qry)
            {
                customers.Add(item.cu);
            }

            customers.Add(new Customer {
                CustomerId=0,
                FirstName = "[Select a customer...]" 
            });
            
            return customers.OrderBy(d => d.FirstName).ThenBy(c => c.LastName).ToList();
        }

        public static List<Tax> GetTaxes(int companyId)
        {
            var taxes = db.Taxes.Where(t => t.CompanyId == companyId).ToList();
            taxes.Add(new Tax
            {
                TaxId = 0,
                Description = "[Select a tax...]"
            });

            return taxes.OrderBy(d => d.Description).ToList();
        }

        public static List<Category> GetCategories(int companyId)
        {
            var categories = db.Categories.Where(c => c.CompanyId == companyId).ToList();
            categories.Add(new Category
            {
                CategoryId = 0,
                Description = "[Select a category...]"
            });

            return categories.OrderBy(d => d.Description).ToList();
        }
        
        
        public void Dispose()
        {
            db.Dispose();
        }
    }
}