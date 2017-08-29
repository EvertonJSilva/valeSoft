﻿using ProjetoModeloDDD.Domain.Entities;


namespace ProjetoModeloDDD.Application.Interface
{
    public interface ITaxaExtraProfissionalAppService : IAppServiceBase<TaxaExtraProfissional>
    {
        TaxaExtraProfissional GetPorIdTaxaExtraProfissional(int id);
    }
}
