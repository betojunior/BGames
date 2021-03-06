﻿using BGames.Enum;
using BGames.Extensions;
using BGames.Domain.Resorces;
using BGames.ValuesObjets;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using BGames.Domain.Entiteis.Base;

namespace BGames.Entiteis
{
    public class Jogador : EntityBase
    {
        public Jogador()
        {

        }
        public Jogador(Email email, string senha)
        {
            Email = email;
            Senha = senha;

            new AddNotifications<Jogador>(this)
                .IfNullOrInvalidLength(x => x.Senha, 6, 32,"A senha deve ter entre 6 e 32 caracteres");

            if (IsInvalid())
            {
                Senha = Senha.ConvertToMD5();
            }
        }

        public Jogador(Nome nome, Email email, string senha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Status = Enum.EnumSituacaoJogador.EmAnalise;

            new AddNotifications<Jogador>(this)
                .IfNullOrInvalidLength(x => x.Senha, 6, 32,
                Message.X0_E_OBRIGATORIO_E_DEVE_CONTER_ENTRE_X1_E_X2_CARACTERES.ToFormat("Senha", "6", "32"));

            if (IsInvalid())
            {
                Senha = Senha.ConvertToMD5();
            }
            
            
            AddNotifications(nome, email);
        }

        public void AlterarJogador(Nome nome, Email email, EnumSituacaoJogador status)
        {
            Nome = nome;
            Email = email;

            new AddNotifications<Jogador>(this).IfFalse(Status == EnumSituacaoJogador.Ativo,"Só é possível alterar jogador sem ele estiver ativo");

            AddNotifications(nome, email);
        }

        public Nome Nome { get; private set; }
        public Email Email { get; private set; }
        public string Senha { get; private set; }
        public EnumSituacaoJogador Status { get; private set; }
    }
}
