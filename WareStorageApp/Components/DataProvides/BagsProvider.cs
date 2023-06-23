﻿using BagApp.Components.CsvReader.Models;
using BagApp.Data.Repositories;

namespace BagApp.Components.DataProvides
{
    public class BagsProvider : IBagsProvider
    {
        private readonly IRepository<Bag> _bagsRepository;

        public BagsProvider(IRepository<Bag> bagsRepository)
        {
            _bagsRepository = bagsRepository;
        }

        public List<Bag[]> ChunkBags(int id)
        {
            var bags = _bagsRepository.GetAll();
            return bags.Chunk(id).ToList();
        }

        public List<Bag> GetAllNames()
        {
            var bags = _bagsRepository.GetAll();
            var list = bags.Select(bag => new Bag
            {
                Id = bag.Id,
                Name = bag.Name,
            }).ToList();
            return list;
        }

        public decimal? GetMostExpensiveBag()
        {
            var bags = _bagsRepository.GetAll();
            return bags.Select(x => x.Price).Max();
        }

        public List<Bag> OrderByBrandAndName()
        {
            var bags = _bagsRepository.GetAll();
            return bags
                .OrderBy(x => x.Brand)
                .ThenBy(x => x.Name)
                .ToList();
        }

        public List<Bag> OrderByName()
        {
            var bags = _bagsRepository.GetAll();
            return bags.OrderBy(x => x.Name).ToList();
        }

        public List<Bag> OrderByNameDesc()
        {
            var bags = _bagsRepository.GetAll();
            return bags.OrderByDescending(x => x.Name).ToList();
        }

        public List<Bag> Take5CheapestBags()
        {
            var bags = _bagsRepository.GetAll();
            return bags
                .OrderBy(x => x.Price)
                .Take(5)
                .ToList();
        }

        public List<Bag> Take5ExpensiveBags()
        {
            var bags = _bagsRepository.GetAll();
            return bags
                .OrderByDescending(x => x.Price)
                .Take(5)
                .ToList();
        }


        public List<Bag> WhereYearIs(int year)
        {
            var bags = _bagsRepository.GetAll();
            return bags.Where(x => x.Year == year).ToList();
        }

        public List<Bag> WhereBrandIs(string brand)
        {
            var bags = _bagsRepository.GetAll();
            return bags.Where(x => x.Brand == brand).ToList();
        }

        public List<Bag> WhereCostIsEmpty()
        {
            var bags = _bagsRepository.GetAll();
            return bags.Where(x => x.Price == 0 || x.Price == null).ToList();
        }


        public List<Bag> WhereStartsWith(string prefix)
        {
            var bags = _bagsRepository.GetAll();
            return bags.Where(x => x.Name.StartsWith(prefix)).ToList();
        }
    }
}