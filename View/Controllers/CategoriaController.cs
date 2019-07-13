﻿using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class CategoriaController : Controller
    {
        private CategoriaRepository repository;

        // Construtor tem com objetivo inicializar objetos ou tipos primitivos
        // necessários para o devido funcionamento da classe sempre é executado
        // o construtor ao instaciar um objeto da classe ou seja 
        // ao fazer 'new CategoriaController()'
        public CategoriaController()
        {
            
            repository = new CategoriaRepository();
        }

        // GET: Categoria
        public ActionResult Index()
        {
            List<Categoria> categorias = repository.ObterTodos();
            ViewBag.Categorias = categorias;
            return View();
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult Store(string nome)
        {
            Categoria categoria = new Categoria();
            categoria.Nome = nome;
            repository.Inserir(categoria);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            Categoria categoria = repository.ObterPeloId(id);
            ViewBag.Categoria = categoria;
            return View();
        }

        public ActionResult Update(int id, string nome)
        {
            Categoria categoria = new Categoria();
            categoria.Id = id;
            categoria.Nome = nome;

            repository.Alterar(categoria);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }
    }
}