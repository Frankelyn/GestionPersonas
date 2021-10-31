using GestionPersonas.DAL;
using GestionPersonas.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GestionPersonas.BLL
{
    public class TipoDeAportesBLL
    {
        public static bool Guardar(TiposDeAportes tAporte)
        {
            if (!Existe(tAporte.TipoDeAporteId))
            {
                return Insertar(tAporte);
            }
            else
            {
                return Modificar(tAporte);
            }
        }

        private static bool Insertar(TiposDeAportes tAporte)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.TiposDeAportes.Add(tAporte);
                paso = contexto.SaveChanges() > 0;
            }
                
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        private static bool Modificar(TiposDeAportes tAporte)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(tAporte).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var tAporte = contexto.TiposDeAportes.Find(id);
                if(tAporte != null)
                {
                    contexto.TiposDeAportes.Remove(tAporte);
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static TiposDeAportes Buscar (int id)
        {
            Contexto contexto = new Contexto();
            TiposDeAportes tAporte = new TiposDeAportes();

            try
            {
                tAporte = contexto.TiposDeAportes.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return tAporte;
        }

        public static List<TiposDeAportes> GetList(Expression<Func<TiposDeAportes, bool>> criterio)
        {
            List<TiposDeAportes> lista = new List<TiposDeAportes>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.TiposDeAportes.Where(criterio).ToList();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }

        public static List<TiposDeAportes> GetTipoDeAportes()
        {
            List<TiposDeAportes> lista = new List<TiposDeAportes>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.TiposDeAportes.ToList();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.TiposDeAportes.Any(e => e.TipoDeAporteId == id);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }
    }
}
