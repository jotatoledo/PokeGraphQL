// <copyright file="PokemonSpeciesType.cs" company="PokeGraphQL.Net">
// Copyright (c) PokeGraphQL.Net. All rights reserved.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace PokeGraphQL.GraphQL.Resources.Pokemons
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using HotChocolate.Types;
    using PokeAPI;
    using PokeGraphQL.GraphQL.Resources.Evolutions;
    using PokeGraphQL.GraphQL.Resources.Games;
    using PokeGraphQL.GraphQL.Resources.Languages;
    using PokeGraphQL.GraphQL.Resources.Locations;

    internal sealed class PokemonSpeciesType : BaseNamedApiObjectType<PokemonSpecies>
    {
        /// <inheritdoc/>
        protected override void ConcreteConfigure(IObjectTypeDescriptor<PokemonSpecies> descriptor)
        {
            descriptor.Description(@"A Pokémon Species forms the basis for at least one pokémon. 
                Attributes of a Pokémon species are shared across all varieties of pokémon within the species. 
                A good example is Wormadam; Wormadam is the species which can be found in three different varieties, Wormadam-Trash, Wormadam-Sandy and Wormadam-Plant.");
            descriptor.Ignore(x => x.Descriptions);
            descriptor.Field(x => x.Order)
                .Description(@"The order in which species should be sorted.
                    Based on National Dex order, except families are grouped together and sorted by stage.");
            descriptor.Field(x => x.FemaleToMaleRate)
                .Name("genderRate")
                .Description("The chance of this Pokémon being female, in eighths; or -1 for genderless.");
            descriptor.Field(x => x.CaptureRate)
                .Description("The base capture rate; up to 255. The higher the number, the easier the catch.");
            descriptor.Field(x => x.BaseHappiness)
                .Description("The happiness when caught by a normal pokéball; up to 255. The higher the number, the happier the pokémon.");
            descriptor.Field(x => x.IsBaby)
                .Description("Whether or not this is a baby pokémon.");
            descriptor.Field(x => x.HatchCounter)
                .Description("Initial hatch counter: one must walk 255 × (hatch_counter + 1) steps before this Pokémon's egg hatches, unless utilizing bonuses like Flame Body's.");
            descriptor.Field(x => x.HasGenderDifferences)
                .Description("Whether or not this pokémon can have different genders.");
            descriptor.Field(x => x.FormsAreSwitchable)
                .Description("Whether or not this pokémon has multiple forms and can switch between them.");
            descriptor.Field(x => x.GrowthRate)
                .Description("The rate at which this pokémon species gains levels.")
                .Type<GrowthRateType>()
                .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetGrowthRateAsync(ctx.Parent<PokemonSpecies>().GrowthRate.Name, token));
            descriptor.Field(x => x.PokedexNumbers)
                .Description("A list of pokedexes and the indexes reserved within them for this pokémon species.")
                .Type<ListType<PokemonSpeciesDexEntryType>>();
            descriptor.Field(x => x.EggGroups)
                .Description("A list of egg groups this pokémon species is a member of.")
                .Type<ListType<EggGroupType>>()
                .Resolver((ctx, token) =>
                {
                    var resolver = ctx.Service<PokemonResolver>();
                    var resourceTasks = ctx.Parent<PokemonSpecies>()
                        .EggGroups
                        .Select(eggGroup => resolver.GetEggGroupAsync(eggGroup.Name, token));
                    return Task.WhenAll(resourceTasks);
                });
            descriptor.Field(x => x.Colours)
                .Description("The color of this pokémon for gimmicky pokedex search.")
                .Type<PokemonColorType>()
                .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetPokemonColorAsync(ctx.Parent<PokemonSpecies>().Colours.Name, token));
            descriptor.Field(x => x.Shape)
                .Description("The shape of this pokémon for gimmicky pokedex search.")
                .Type<PokemonShapeType>()
                .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetPokemonShapeAsync(ctx.Parent<PokemonSpecies>().Shape.Name, token));
            descriptor.Field(x => x.EvolvesFromSpecies)
                .Description("The pokémon species that evolves into this species.")
                .Type<PokemonSpeciesType>()
                .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetPokemonSpeciesAsync(ctx.Parent<PokemonSpecies>().EvolvesFromSpecies.Name, token));
            descriptor.Field(x => x.EvolutionChain)
                .Description("The evolution chain this pokémon species is a member of.")
                .Type<EvolutionChainType>()
                .Resolver((ctx, token) => ctx.Service<EvolutionResolver>().GetEvolutionChainAsync(Convert.ToInt32(ctx.Parent<PokemonSpecies>().EvolutionChain.Url.LastSegment()), token));
            descriptor.Field(x => x.Habitat)
                .Description("The habitat this pokémon species can be encountered in.")
                .Type<PokemonHabitatType>()
                .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetPokemonHabitatAsync(ctx.Parent<PokemonSpecies>().Habitat.Name, token));
            descriptor.Field(x => x.Generation)
                .Description("The generation this pokémon species was introduced in.")
                .Type<GenerationType>()
                .Resolver((ctx, token) => ctx.Service<GameResolver>().GetGenerationAsync(ctx.Parent<PokemonSpecies>().Generation.Name, token));
            descriptor.Field(x => x.PalParkEncounters)
                .Description("A list of encounters that can be had with this pokémon species in pal park.")
                .Type<ListType<PalParkEncounterAreaType>>();
            descriptor.Field(x => x.Genera)
                .Description("The genus of this pokémon species listed in multiple languages.")
                .Type<ListType<GenusType>>();
            descriptor.Field(x => x.Varieties)
                .Description("A list of the pokémon that exist within this pokémon species.")
                .Type<ListType<PokemonSpeciesVarietyType>>();
            descriptor.Field(x => x.FlavorTexts)
                .Type<ListType<PokemonSpeciesFlavorTextType>>();
        }

        private sealed class PokemonSpeciesFlavorTextType : ObjectType<PokemonSpeciesFlavorText>
        {
            protected override void Configure(IObjectTypeDescriptor<PokemonSpeciesFlavorText> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.FlavorText)
                    .Name("text")
                    .Description("The text the flavor resource.");
                descriptor.Field(x => x.Version)
                    .Type<VersionType>()
                    .Resolver((ctx, token) => ctx.Service<GameResolver>().GetVersionAsync(ctx.Parent<PokemonSpeciesFlavorText>().Version.Name, token));
                descriptor.Field(x => x.Language)
                    .Description("The language this text is in.")
                    .Type<LanguageType>()
                    .Resolver((ctx, token) => ctx.Service<LanguageResolver>().GetLanguageAsync(ctx.Parent<PokemonSpeciesFlavorText>().Language.Name, token));
            }
        }

        private sealed class PokemonSpeciesVarietyType : ObjectType<PokemonSpeciesVariety>
        {
            protected override void Configure(IObjectTypeDescriptor<PokemonSpeciesVariety> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.IsDefault);
                descriptor.Field(x => x.Pokemon)
                    .Type<PokemonType>()
                    .Resolver((ctx, token) => ctx.Service<PokemonResolver>().GetPokemonAsync(ctx.Parent<PokemonSpeciesVariety>().Pokemon.Name, token));
            }
        }

        private sealed class PokemonSpeciesDexEntryType : ObjectType<PokemonSpeciesDexEntry>
        {
            protected override void Configure(IObjectTypeDescriptor<PokemonSpeciesDexEntry> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.EntryNumber)
                    .Description("The index number within the pokédex.");
                descriptor.Field(x => x.Pokedex)
                    .Description("The pokédex the referenced pokémon species can be found in.")
                    .Type<PokedexType>()
                    .Resolver((ctx, token) => ctx.Service<GameResolver>().GetPokedexAsync(ctx.Parent<PokemonSpeciesDexEntry>().Pokedex.Name, token));
            }
        }

        private sealed class GenusType : ObjectType<Genus>
        {
            protected override void Configure(IObjectTypeDescriptor<Genus> descriptor)
            {
                descriptor.FixStructType();
                descriptor.Field(x => x.Name)
                    .Description("The localized genus for the referenced pokémon species.");
                descriptor.Field(x => x.Language)
                    .Description("The language this genus is in.")
                    .Type<LanguageType>()
                    .Resolver((ctx, token) => ctx.Service<LanguageResolver>().GetLanguageAsync(ctx.Parent<Genus>().Language.Name, token));
            }
        }

        private sealed class PalParkEncounterAreaType : ObjectType<PalParkEncounterArea>
        {
            /// <inheritdoc/>
            protected override void Configure(IObjectTypeDescriptor<PalParkEncounterArea> descriptor)
            {
                descriptor.Field(x => x.BaseScore)
                    .Description("The base score given to the player when the referenced pokemon is caught during a pal park run.");
                descriptor.Field(x => x.Rate)
                    .Description("	The base rate for encountering the referenced pokemon in this pal park area.");
                descriptor.Field(x => x.Area)
                    .Description("The pal park area where this encounter happens.")
                    .Type<PalParkAreaType>()
                    .Resolver((ctx, token) => ctx.Service<LocationResolver>().GetPalParkAreaAsync(ctx.Parent<PalParkEncounterArea>().Area.Name, token));
            }
        }
    }
}
