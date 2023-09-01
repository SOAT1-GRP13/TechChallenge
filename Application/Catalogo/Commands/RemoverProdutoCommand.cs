﻿using Application.Catalogo.Boundaries;
using Application.Catalogo.Commands.Validation;
using Domain.Base.Messages;


namespace Application.Catalogo.Commands
{
    public class RemoverProdutoCommand : Command<bool>
    {

        public RemoverProdutoCommand(Guid id)
        {
            idProduto = id;
        }

        public Guid idProduto { get; set; }

    }
}
