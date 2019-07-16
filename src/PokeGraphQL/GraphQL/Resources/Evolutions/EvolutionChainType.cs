// <copyright file="EvolutionChainType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Evolutions
{
    using HotChocolate.Types;
    using PokeApiNet.Models;
    using PokeGraphQL.GraphQL.Resources.Items;
    using PokeGraphQL.GraphQL.Resources.Locations;
    using PokeGraphQL.GraphQL.Resources.Moves;
    using PokeGraphQL.GraphQL.Resources.Pokemons;

    internal sealed class EvolutionChainType : BaseApiObjectType<EvolutionChain>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<EvolutionChain> descriptor)
        {
            descriptor.Description(@"Evolution chains are essentially family trees. 
                They start with the lowest stage within a family 
                and detail evolution conditions for each as well as pokémon 
                they can evolve into up through the hierarchy.");
            descriptor.UseNullableNamedApiResourceField<EvolutionChain, Item, ItemType>(x => x.BabyTriggerItem);
            descriptor.Field(x => x.Chain)
                .Description(@"The base chain link object. 
                    Each link contains evolution details for a pokémon in the chain. 
                    Each link references the next pokémon in the natural evolution order.")
                .Type<ChainLinkType>();
        }

        private sealed class EvolutionDetailType : ObjectType<EvolutionDetail>
        {
            protected override void Configure(IObjectTypeDescriptor<EvolutionDetail> descriptor)
            {
                descriptor.UseNullableNamedApiResourceField<EvolutionDetail, Item, ItemType>(x => x.Item);
                descriptor.UseNamedApiResourceField<EvolutionDetail, EvolutionTrigger, EvolutionTriggerType>(x => x.Trigger);
                descriptor.Field(x => x.Gender)
                    .Description("The gender the evolving pokémon species must be in order to evolve into this pokémon species.");
                descriptor.UseNullableNamedApiResourceField<EvolutionDetail, Item, ItemType>(x => x.HeldItem);
                descriptor.UseNullableNamedApiResourceField<EvolutionDetail, Move, MoveType>(x => x.KnownMove);
                descriptor.UseNullableNamedApiResourceField<EvolutionDetail, Type, TypePropertyType>(x => x.KnownMoveType);
                descriptor.UseNullableNamedApiResourceField<EvolutionDetail, Location, LocationType>(x => x.Location);
                descriptor.Field(x => x.MinLevel)
                    .Description("The minimum required level of the evolving pokémon species to evolve into this pokémon species.");
                descriptor.Field(x => x.MinHappiness)
                    .Description("The minimum required level of happiness the evolving pokémon species to evolve into this pokémon species.");
                descriptor.Field(x => x.MinBeauty)
                    .Description("The minimum required level of beauty the evolving pokémon species to evolve into this pokémon species.");
                descriptor.Field(x => x.MinAffection)
                    .Description("The minimum required level of affection the evolving pokémon species to evolve into this pokémon species.");
                descriptor.Field(x => x.NeedsOverworldRain)
                    .Description("Whether or not it must be raining in the overworld to cause evolution this pokémon species.");
                descriptor.UseNullableNamedApiResourceField<EvolutionDetail, PokemonSpecies, PokemonSpeciesType>(x => x.PartySpecies);
                descriptor.UseNullableNamedApiResourceField<EvolutionDetail, Type, TypePropertyType>(x => x.PartyType);
                descriptor.Field(x => x.RelativePhysicalStats)
                    .Description("The required relation between the Pokémon's Attack and Defense stats. 1 means Attack > Defense. 0 means Attack = Defense. -1 means Attack < Defense.");
                descriptor.Field(x => x.TimeOfDay)
                    .Description("The required time of day. Day or night.");
                descriptor.UseNullableNamedApiResourceField<EvolutionDetail, PokemonSpecies, PokemonSpeciesType>(x => x.TradeSpecies);
                descriptor.Field(x => x.TurnUpsideDown)
                    .Description("Whether or not the 3DS needs to be turned upside-down as this Pokémon levels up.");
            }
        }

        private sealed class ChainLinkType : ObjectType<ChainLink>
        {
            protected override void Configure(IObjectTypeDescriptor<ChainLink> descriptor)
            {
                descriptor.Field(x => x.IsBaby)
                    .Description(@"Whether or not this link is for a baby pokémon. 
                        This would only ever be true on the base link.");
                descriptor.UseNamedApiResourceField<ChainLink, PokemonSpecies, PokemonSpeciesType>(x => x.Species);
                descriptor.Field(x => x.EvolutionDetails)
                    .Description("All details regarding the specific details of the referenced pokémon species evolution.")
                    .Type<ListType<EvolutionDetailType>>();
                descriptor.Field(x => x.EvolvesTo)
                    .Description("A List of chain objects.")
                    .Type<ListType<ChainLinkType>>();
            }
        }
    }
}
