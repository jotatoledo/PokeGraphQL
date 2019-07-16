// <copyright file="TypePropertyType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Common;
    using PokeGraphQL.GraphQL.Resources.Games;
    using PokeGraphQL.GraphQL.Resources.Moves;
    using TypeProperty = PokeApiNet.Models.Type;

    internal sealed class TypePropertyType : BaseNamedApiObjectType<TypeProperty>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<TypeProperty> descriptor)
        {
            descriptor.Description(@"Types are properties for Pokémon and their moves. 
                Each type has three properties: which types of Pokémon it is super effective against, which types of Pokémon it is not very effective against
                and which types of Pokémon it is completely ineffective against.");
            descriptor.Field(x => x.DamageRelations)
                .Description("A detail of how effective this type is toward others and vice versa.")
                .Type<TypeRelationsType>();
            descriptor.Field(x => x.GameIndices)
                .Description("A list of game indices relevent to this item by generation.")
                .Type<ListType<GenerationGameIndexType>>();
            descriptor.UseNamedApiResourceField<TypeProperty, Generation, GenerationType>(x => x.Generation);
            descriptor.UseNamedApiResourceField<TypeProperty, MoveDamageClass, MoveDamageClassType>(x => x.MoveDamageClass);
            descriptor.Field(x => x.Pokemon)
                .Description("A list of details of pokemon that have this type.")
                .Type<ListType<TypePokemonType>>();
            descriptor.UseNamedApiResourceCollectionField<TypeProperty, Move, MoveType>(x => x.Moves);
        }

        private sealed class TypeRelationsType : ObjectType<TypeRelations>
        {
            protected override void Configure(IObjectTypeDescriptor<TypeRelations> descriptor)
            {
                descriptor.UseNamedApiResourceCollectionField<TypeRelations, TypeProperty, TypePropertyType>(x => x.NoDamageTo);
                descriptor.UseNamedApiResourceCollectionField<TypeRelations, TypeProperty, TypePropertyType>(x => x.HalfDamageTo);
                descriptor.UseNamedApiResourceCollectionField<TypeRelations, TypeProperty, TypePropertyType>(x => x.DoubleDamageTo);
                descriptor.UseNamedApiResourceCollectionField<TypeRelations, TypeProperty, TypePropertyType>(x => x.NoDamageFrom);
                descriptor.UseNamedApiResourceCollectionField<TypeRelations, TypeProperty, TypePropertyType>(x => x.HalfDamageFrom);
                descriptor.UseNamedApiResourceCollectionField<TypeRelations, TypeProperty, TypePropertyType>(x => x.DoubleDamageFrom);
            }
        }

        private sealed class TypePokemonType : ObjectType<TypePokemon>
        {
            protected override void Configure(IObjectTypeDescriptor<TypePokemon> descriptor)
            {
                descriptor.Field(x => x.Slot)
                    .Description("The order the pokemons types are listed in.");
                descriptor.UseNamedApiResourceField<TypePokemon, Pokemon, PokemonType>(x => x.Pokemon);
            }
        }
    }
}
