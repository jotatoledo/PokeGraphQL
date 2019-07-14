// <copyright file="EvolutionChainType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Evolutions
{
    using System.Threading.Tasks;
    using DotNetFunctional.Maybe;
    using HotChocolate.Types;
    using PokeAPI;
    using PokeGraphQL.GraphQL.Resources.Items;
    using PokeGraphQL.GraphQL.Resources.Locations;
    using PokeGraphQL.GraphQL.Resources.Moves;
    using PokeGraphQL.GraphQL.Resources.Pokemons;
    using MonadMaybe = DotNetFunctional.Maybe.Maybe;

    internal sealed class EvolutionChainType : BaseApiObjectType<EvolutionChain>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<EvolutionChain> descriptor)
        {
            descriptor.Description(@"Evolution chains are essentially family trees. 
                They start with the lowest stage within a family 
                and detail evolution conditions for each as well as pokémon 
                they can evolve into up through the hierarchy.");
            descriptor.Field(x => x.BabyTriggerItem)
                .Description(@"The item that a pokémon would be holding when mating 
                    that would trigger the egg hatching a baby pokémon rather than a basic pokémon.")
                .Type<ItemType>()
                .Resolver((ctx, token) => MonadMaybe.Lift(ctx.Parent<EvolutionChain>().BabyTriggerItem)
                    .Select(x => x.Name)
                    .Match(name => ctx.Service<ItemResolver>().GetItemAsync(name, token), Task.FromResult<Item>(default)));
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
                descriptor.FixStructType();
                descriptor.Field(x => x.Item)
                    .Description("The item required to cause evolution this into pokémon species.")
                    .Type<ItemType>()
                    .Resolver((ctx, token) => MonadMaybe.Lift(ctx.Parent<EvolutionDetail>().Item)
                        .Select(x => x.Name)
                        .Match(name => ctx.Service<ItemResolver>().GetItemAsync(name, token), Task.FromResult<Item>(default)));
                descriptor.Field(x => x.Trigger)
                    .Description("The type of event that triggers evolution into this pokémon species.")
                    .Type<EvolutionTriggerType>()
                    .Resolver((ctx, token) => ctx.Service<EvolutionResolver>().GetEvolutionTriggerAsync(ctx.Parent<EvolutionDetail>().Trigger.Name, token));
                descriptor.Field(x => x.Gender)
                    .Description("The gender the evolving pokémon species must be in order to evolve into this pokémon species.");
                descriptor.Field(x => x.HeldItem)
                    .Description("The item the evolving pokémon species must be holding during the evolution trigger event to evolve into this pokémon species.")
                    .Type<ItemType>()
                    .Resolver((ctx, token) => MonadMaybe.Lift(ctx.Parent<EvolutionDetail>().HeldItem)
                        .Select(x => x.Name)
                        .Match(name => ctx.Service<ItemResolver>().GetItemAsync(name, token), Task.FromResult<Item>(default)));
                descriptor.Field(x => x.KnownMove)
                    .Description("The move that must be known by the evolving pokémon species during the evolution trigger event in order to evolve into this pokémon species.")
                    .Type<MoveType>()
                    .Resolver((ctx, token) => MonadMaybe.Lift(ctx.Parent<EvolutionDetail>().KnownMove)
                        .Select(x => x.Name)
                        .Match(name => ctx.Service<MoveResolver>().GetMoveAsync(name, token), Task.FromResult<Move>(default)));
                descriptor.Field(x => x.KnownMoveType)
                    .Description("The evolving pokémon species must know a move with this type during the evolution trigger event in order to evolve into this pokémon species.")
                    .Type<TypePropertyType>()
                    .Resolver((ctx, token) => MonadMaybe.Lift(ctx.Parent<EvolutionDetail>().KnownMoveType)
                        .Select(x => x.Name)
                        .Match(name => ctx.Service<PokemonResolver>().GetTypeAsync(name, token), Task.FromResult<PokeAPI.PokemonType>(default)));
                descriptor.Field(x => x.Location)
                    .Description("The location the evolution must be triggered at.")
                    .Type<LocationType>()
                    .Resolver((ctx, token) => MonadMaybe.Lift(ctx.Parent<EvolutionDetail>().Location)
                        .Select(x => x.Name)
                        .Match(name => ctx.Service<LocationResolver>().GetLocationAsync(name, token), Task.FromResult<Location>(default)));
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
                descriptor.Field(x => x.PartySpecies)
                    .Description("The pokémon species that must be in the players party in order for the evolving pokémon species to evolve into this pokémon species.")
                    .Type<PokemonSpeciesType>()
                    .Resolver((ctx, token) => MonadMaybe.Lift(ctx.Parent<EvolutionDetail>().PartySpecies)
                        .Select(x => x.Name)
                        .Match(name => ctx.Service<PokemonResolver>().GetPokemonSpeciesAsync(name, token), Task.FromResult<PokemonSpecies>(default)));
                descriptor.Field(x => x.PartyType)
                    .Description("The player must have a pokémon of this type in their party during the evolution trigger event in order for the evolving pokémon species to evolve into this pokémon species.")
                    .Type<TypePropertyType>()
                    .Resolver((ctx, token) => MonadMaybe.Lift(ctx.Parent<EvolutionDetail>().PartyType)
                        .Select(x => x.Name)
                        .Match(name => ctx.Service<PokemonResolver>().GetTypeAsync(name, token), Task.FromResult<PokeAPI.PokemonType>(default)));
                descriptor.Field(x => x.RelativePhysicalStats)
                    .Description("The required relation between the Pokémon's Attack and Defense stats. 1 means Attack > Defense. 0 means Attack = Defense. -1 means Attack < Defense.");
                descriptor.Field(x => x.TimeOfDay)
                    .Description("The required time of day. Day or night.");
                descriptor.Field(x => x.TradeSpecies)
                    .Description("Pokémon species for which this one must be traded.")
                    .Type<PokemonSpeciesType>()
                    .Resolver((ctx, token) => MonadMaybe.Lift(ctx.Parent<EvolutionDetail>().TradeSpecies)
                        .Select(x => x.Name)
                        .Match(name => ctx.Service<PokemonResolver>().GetPokemonSpeciesAsync(name, token), Task.FromResult<PokemonSpecies>(default)));
                descriptor.Field(x => x.TurnUpsideDown)
                    .Description("Whether or not the 3DS needs to be turned upside-down as this Pokémon levels up.");
            }
        }

        private sealed class ChainLinkType : ObjectType<ChainLink>
        {
            protected override void Configure(IObjectTypeDescriptor<ChainLink> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.IsBaby)
                    .Description(@"Whether or not this link is for a baby pokémon. 
                        This would only ever be true on the base link.");
                descriptor.Field(x => x.Species)
                    .Description("The pokemon species at this point in the evolution chain.")
                    .Type<PokemonSpeciesType>()
                    .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetPokemonSpeciesAsync(ctx.Parent<ChainLink>().Species.Name, token));
                descriptor.Field(x => x.Details)
                    .Description("All details regarding the specific details of the referenced pokémon species evolution.")
                    .Type<ListType<EvolutionDetailType>>();
                descriptor.Field(x => x.EvolvesTo)
                    .Description("A List of chain objects.")
                    .Type<ListType<ChainLinkType>>();
            }
        }
    }
}
