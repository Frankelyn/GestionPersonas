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
    public class AportesBLL
    {
        public static bool Guardar(Aportes Aporte)
        {
            if (!Existe(Aporte.AporteId))
                return Insertar(Aporte);
            else
                return Modificar(Aporte);
        }

        private static bool Insertar(Aportes Aporte)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                

                foreach (var detalle in Aporte.AportesDetalle)
                {
                    detalle.TipoDeAporte.Logrado += detalle.Aporte;
                    detalle.TipoDeAporte.Meta -= detalle.Aporte;

                    contexto.Entry(detalle.TipoDeAporte).State = EntityState.Modified;
                   
                }

                contexto.Aportes.Add(Aporte);

                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        private static bool Modificar(Aportes Aporte)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var aporteAnterior = contexto.Aportes
                    .Where(x => x.AporteId == Aporte.AporteId)
                    .Include(x => x.AportesDetalle)
                    .ThenInclude(x => x.TipoDeAporte)
                    .AsNoTracking()
                    .SingleOrDefault();

                foreach (var detalle in aporteAnterior.AportesDetalle)
                {
                    detalle.TipoDeAporte.Logrado -= detalle.Aporte;
                    detalle.TipoDeAporte.Meta += detalle.Aporte;
                }

                contexto.Database.ExecuteSqlRaw($"Delete From AportesDetalle where AporteDetalleId = {Aporte.AporteId}");

                foreach (var Item in Aporte.AportesDetalle)
                {
                    Item.TipoDeAporte.Logrado += Item.Aporte;
                    Item.TipoDeAporte.Meta -= Item.Aporte;

                    
                    contexto.Entry(Item).State = EntityState.Added;

                }

                
                contexto.Entry(Aporte).State = EntityState.Modified;
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
                var Aporte = AportesBLL.Buscar(id);

                if (Aporte != null)
                {
                    foreach (var detalle in Aporte.AportesDetalle)
                    {
                        detalle.TipoDeAporte.Logrado -= detalle.Aporte;
                        detalle.TipoDeAporte.Meta += detalle.Aporte;

                        contexto.Entry(detalle.TipoDeAporte).State = EntityState.Modified;
                    }

                    contexto.Aportes.Remove(Aporte);
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

        public static Aportes Buscar(int id)
        {
            Aportes Aporte = new Aportes();
            Contexto contexto = new Contexto();

            try
            {
                Aporte = contexto.Aportes.Include(x => x.AportesDetalle)
                    .Where(x => x.AporteId == id)
                    .Include(x => x.AportesDetalle)
                    .ThenInclude(x => x.TipoDeAporte)
                    .SingleOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return Aporte;
        }


        public static List<Aportes> Getlist(Expression<Func<Aportes, bool>> criterio)
        {
            List<Aportes> lista = new List<Aportes>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Aportes.Where(criterio).ToList();
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
                encontrado = contexto.Aportes.Any(e => e.AporteId == id);
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
