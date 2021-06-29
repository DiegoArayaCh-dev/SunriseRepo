﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryProducto
    {
        public IEnumerable<PRODUCTOS> GetProductos()
        {
            try
            {
                IEnumerable<PRODUCTOS> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //lista = ctx.Libro.Include("PRODUCTOS").ToList();
                    lista = ctx.PRODUCTOS.Include("CATEGORIA").ToList();

                }
                return lista;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public PRODUCTOS GetProductoByID(int pID)
        {
            try
            {
                PRODUCTOS oProducto = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    oProducto = ctx.PRODUCTOS.
                            Include("CATEGORIA").
                            Include("PROVEEDORES").
                            Include("PROVEEDORES.PAIS").
                            Include("ProdSuc").
                            Include("ProdSuc.SUCURSAL").
                                Where(p => p.ID == pID).
                                    FirstOrDefault<PRODUCTOS>();
                }
                return oProducto;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }

        }

        public IEnumerable<PRODUCTOS> GetProductoByNombre(string pFiltro)
        {
            try
            {
                IEnumerable<PRODUCTOS> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.PRODUCTOS.Include("CATEGORIA").ToList().
                         FindAll(l => l.nombre.ToLower().Contains(pFiltro.ToLower()));
                }
                return lista;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<CATEGORIA> GetCategorias()
        {
            try
            {
                IEnumerable<CATEGORIA> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.CATEGORIA.ToList<CATEGORIA>();
                }
                return lista;

            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<SUCURSAL> GetSucursales()
        {
            try
            {
                IEnumerable<SUCURSAL> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.SUCURSAL.ToList<SUCURSAL>();
                }
                return lista;

            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public SUCURSAL GetSucursalesByID(int id)
        {
            SUCURSAL sucursal = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    sucursal = ctx.SUCURSAL.Find(id);
                }

                return sucursal;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<PROVEEDORES> GetProveedores()
        {
            try
            {
                IEnumerable<PROVEEDORES> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.PROVEEDORES.ToList<PROVEEDORES>();
                }
                return lista;

            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public PROVEEDORES GetProveedoresByID(int id)
        {
            PROVEEDORES proveedor = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    proveedor = ctx.PROVEEDORES.Find(id);
                }

                return proveedor;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public PRODUCTOS Save(PRODUCTOS pProducto, string[] selectedProveedores)
        {
            int retorno = 0;
            PRODUCTOS oProducto = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oProducto = GetProductoByID((int)pProducto.ID);

                    if (oProducto == null)
                    {
                        using (var transaccion = ctx.Database.BeginTransaction())
                        {
                            ctx.PRODUCTOS.Add(pProducto);
                            retorno = ctx.SaveChanges();

                            //Insertar
                            if (selectedProveedores != null)
                            {
                                pProducto.PROVEEDORES = new List<PROVEEDORES>();
                                foreach (var proveedor in selectedProveedores)
                                {
                                    var proveedorToAdd = GetProveedoresByID(int.Parse(proveedor));
                                    ctx.PROVEEDORES.Attach(proveedorToAdd); //sin esto, EF intentará crear una categoría
                                    pProducto.PROVEEDORES.Add(proveedorToAdd);// asociar a la categoría existente con el libro

                                    retorno = ctx.SaveChanges();

                                }
                            }

                            // Commit 
                            transaccion.Commit();
                        }

                    }
                    else
                    {

                        //Registradas: 1,2,3
                        //Actualizar: 1,3,4

                        //Actualizar Producto
                        ctx.PRODUCTOS.Add(pProducto);
                        ctx.Entry(pProducto).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();

                        //Actualizar Proveedores
                        var selectedProveedoresID = new HashSet<string>(selectedProveedores);
                        if (selectedProveedores != null)
                        {
                            ctx.Entry(pProducto).Collection(p => p.PROVEEDORES).Load();

                            var newProveedorForProducto = ctx.PROVEEDORES
                             .Where(x => selectedProveedoresID.Contains(x.ID.ToString())).ToList();

                            pProducto.PROVEEDORES = newProveedorForProducto;
                            ctx.Entry(pProducto).State = EntityState.Modified;
                            retorno = ctx.SaveChanges();
                        }

                    }
                }


                if (retorno >= 0)
                    oProducto = GetProductoByID((int)pProducto.ID);

                return oProducto;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }

        public PRODUCTOS Save_AUX(PRODUCTOS pProducto, string[] selectedSucursales, string[] selectedProveedores)
        {
            int retorno = 0;
            PRODUCTOS oProducto = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oProducto = GetProductoByID((int)pProducto.ID);

                    if (oProducto == null)
                    {
                        using (var transaccion = ctx.Database.BeginTransaction())
                        {
                            ctx.PRODUCTOS.Add(pProducto);
                            retorno = ctx.SaveChanges();

                            //Insertar
                            if (selectedProveedores != null)
                            {
                                pProducto.PROVEEDORES = new List<PROVEEDORES>();
                                foreach (var proveedor in selectedProveedores)
                                {
                                    var proveedorToAdd = GetProveedoresByID(int.Parse(proveedor));
                                    ctx.PROVEEDORES.Attach(proveedorToAdd); //sin esto, EF intentará crear una categoría
                                    pProducto.PROVEEDORES.Add(proveedorToAdd);// asociar a la categoría existente con el libro

                                    retorno = ctx.SaveChanges();

                                }
                            }

                            if (selectedSucursales != null)
                            {
                                pProducto.ProdSuc = new List<ProdSuc>();
                                foreach (var sucursales in selectedSucursales)
                                {
                                    var sucursalGenerica = GetSucursalesByID(int.Parse(sucursales));

                                    ProdSuc psGenerico = new ProdSuc();
                                    psGenerico.IDProducto = pProducto.ID;
                                    psGenerico.IDSucursal = sucursalGenerica.ID;
                                    psGenerico.cant = 0;

                                    //ctx.ProdSuc.Attach(psGenerico); //sin esto, EF intentará crear una categoría
                                    pProducto.ProdSuc.Add(psGenerico);// asociar a la categoría existente con el libro

                                    retorno = ctx.SaveChanges();

                                }
                            }

                            // Commit 
                            transaccion.Commit();
                        }

                    }
                    else
                    {

                        //Registradas: 1,2,3
                        //Actualizar: 1,3,4

                        //Actualizar Producto
                        ctx.PRODUCTOS.Add(pProducto);
                        ctx.Entry(pProducto).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();

                        //Actualizar Proveedores
                        var selectedProveedoresID = new HashSet<string>(selectedProveedores);
                        if (selectedProveedores != null)
                        {
                            ctx.Entry(pProducto).Collection(p => p.PROVEEDORES).Load();
                            var newProveedorForProducto = ctx.PROVEEDORES
                             .Where(x => selectedProveedoresID.Contains(x.ID.ToString())).ToList();

                            pProducto.PROVEEDORES = newProveedorForProducto;
                            ctx.Entry(pProducto).State = EntityState.Modified;
                            retorno = ctx.SaveChanges();
                        }
                        if (selectedSucursales != null)
                        {
                            //Actualizar Sucursales
                            var selectedSucarsalesID = new HashSet<string>(selectedSucursales);
                            if (selectedSucursales != null)
                            {
                                ctx.Entry(pProducto).Collection(p => p.ProdSuc).Load();

                                var new_PS_ForProducto = ctx.SUCURSAL
                                 .Where(x => selectedSucarsalesID.Contains(x.ID.ToString())).ToList();
                                ICollection<ProdSuc>  insertPS = new List<ProdSuc>();
                                foreach (SUCURSAL suc in new_PS_ForProducto)
                                {
                                    ProdSuc s = new ProdSuc();
                                    s.IDSucursal = suc.ID;
                                    s.IDProducto = oProducto.ID;
                                    s.cant = 0;
                                    foreach(ProdSuc ps in oProducto.ProdSuc)
                                    {
                                        if (ps.IDSucursal == suc.ID) s.cant = ps.cant;
                                    }
                                    insertPS.Add(s);
                                }
                                pProducto.ProdSuc = insertPS;
                                ctx.Entry(pProducto).State = EntityState.Modified;

                                retorno = ctx.SaveChanges();
                            }



                            //PRODUCTOS originalProducto = GetProductoByID(pProducto.ID);
                            //foreach (string sucursal in selectedSucursales)
                            //{
                            //    //  var sucursalGenerica = GetSucursalesByID(int.Parse(sucursales));

                            //    //ProdSuc psGenerico = new ProdSuc();
                            //    //psGenerico.IDProducto = pProducto.ID;
                            //    //psGenerico.IDSucursal = int.Parse(sucursales);

                                


                            //        ////Si no lo contiene, que lo agregue
                            //        //foreach (var item in originalProducto.ProdSuc.)
                            //        //{
                            //        //    //if (item.IDSucursal == psGenerico.IDSucursal &&
                            //        //    //        item.IDProducto == psGenerico.IDProducto)
                            //        //    if(item.IDSucursal!=Convert.ToInt32(sucursal)){
                            //        //        psGenerico.cant = 0;
                            //        //        pProducto.ProdSuc.Add(psGenerico); // agrega la relacion
                            //        //                                           // ctx.Entry(pProducto).State = EntityState.Modified;
                            //        //        retorno = ctx.SaveChanges();
                            //        //    }
                            //        //}
                            //}


                            //}

                            ////Si ya no existe, que lo borre
                            //ICollection<ProdSuc> listaTemporalPS_Nueva = new List<ProdSuc>(); //Lista de objetos de VIEW
                            //ICollection<ProdSuc> listaTemporalPS_Vieja = pProducto.ProdSuc; //Lista de objetos en BD

                            ////Guardo y construyo los objetos de la VIEW
                            //foreach (var sucursales in selectedSucursales)
                            //{
                            //    var sucursalGenerica = GetSucursalesByID(int.Parse(sucursales));
                            //    ProdSuc psGenerico = new ProdSuc();
                            //    psGenerico.IDProducto = pProducto.ID;
                            //    psGenerico.IDSucursal = sucursalGenerica.ID;
                            //    psGenerico.cant = 0;
                            //    listaTemporalPS_Nueva.Append(psGenerico);
                            //}

                            ////Si ya no existe en el VIEW, lo elimine de la lista de BD
                            //foreach (var psItemViejo in listaTemporalPS_Vieja)
                            //{
                            //    if (!listaTemporalPS_Nueva.Contains(psItemViejo))
                            //    {
                            //        pProducto.ProdSuc.Remove(psItemViejo);
                            //        ctx.Entry(pProducto).State = EntityState.Modified;
                            //        retorno = ctx.SaveChanges();
                            //    }
                            //}
                        }

                    }

                    if (retorno >= 0)
                        oProducto = GetProductoByID((int)pProducto.ID);

                    return oProducto;
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }

        public PRODUCTOS Save_AUX_DESCARTADA_SOLO_CODIGO_SIRVE(PRODUCTOS pProducto, string[] selectedSucursales, string[] selectedProveedores)
        {
            int retorno = 0;
            PRODUCTOS oProducto = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oProducto = GetProductoByID((int)pProducto.ID);

                    if (oProducto == null)
                    {
                        using (var transaccion = ctx.Database.BeginTransaction())
                        {
                            ctx.PRODUCTOS.Add(pProducto);
                            retorno = ctx.SaveChanges();

                            //Insertar
                            if (selectedProveedores != null)
                            {
                                pProducto.PROVEEDORES = new List<PROVEEDORES>();
                                foreach (var proveedor in selectedProveedores)
                                {
                                    var proveedorToAdd = GetProveedoresByID(int.Parse(proveedor));
                                    ctx.PROVEEDORES.Attach(proveedorToAdd); //sin esto, EF intentará crear una categoría
                                    pProducto.PROVEEDORES.Add(proveedorToAdd);// asociar a la categoría existente con el libro

                                    retorno = ctx.SaveChanges();

                                }
                            }

                            if (selectedSucursales != null)
                            {
                                pProducto.ProdSuc = new List<ProdSuc>();
                                foreach (var sucursalID in selectedSucursales)
                                {
                                    var sucursalGenerica = GetSucursalesByID(int.Parse(sucursalID));

                                    ProdSuc psGenerico = new ProdSuc();
                                    psGenerico.IDProducto = pProducto.ID;
                                    psGenerico.IDSucursal = sucursalGenerica.ID;
                                    //psGenerico.cant = 0;

                                    //ctx.ProdSuc.Attach(psGenerico); //sin esto, EF intentará crear una categoría
                                    pProducto.ProdSuc.Add(psGenerico);// asociar a la categoría existente con el libro

                                    retorno = ctx.SaveChanges();

                                }
                            }
                            // Commit 
                            transaccion.Commit();
                        }

                    }
                    else
                    {

                        //Registradas: 1,2,3
                        //Actualizar: 1,3,4

                        //Actualizar Producto
                        ctx.PRODUCTOS.Add(pProducto);
                        ctx.Entry(pProducto).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();

                        //Actualizar Proveedores
                        var selectedProveedoresID = new HashSet<string>(selectedProveedores);
                        if (selectedProveedores != null)
                        {
                            ctx.Entry(pProducto).Collection(p => p.PROVEEDORES).Load();

                            var newProveedorForProducto = ctx.PROVEEDORES
                             .Where(x => selectedProveedoresID.Contains(x.ID.ToString())).ToList();

                            pProducto.PROVEEDORES = newProveedorForProducto;
                            ctx.Entry(pProducto).State = EntityState.Modified;
                            retorno = ctx.SaveChanges();
                        }

                        //Actualizar Sucursales
                        var selectedSucarsalesID = new HashSet<string>(selectedSucursales);
                        if (selectedSucursales != null)
                        {
                            ctx.Entry(pProducto).Collection(p => p.ProdSuc).Load();

                            var new_PS_ForProducto = ctx.ProdSuc
                             .Where(x => selectedSucarsalesID.Contains(x.IDSucursal.ToString())).ToList();

                            pProducto.ProdSuc = new_PS_ForProducto;
                            ctx.Entry(pProducto).State = EntityState.Modified;
                            retorno = ctx.SaveChanges();
                        }

                    }
                }


                if (retorno >= 0)
                    oProducto = GetProductoByID((int)pProducto.ID);

                return oProducto;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }

    }

}

