// <copyright file="PokemonSpeciesType.cs" company="PokeGraphQL.Net">
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
            descriptor.Ignore(x => x.FormDescriptions);
            descriptor.Field(x => x.Order)
                .Description(@"The order in which species should be sorted.
                    Based on National Dex order, except families are grouped together and sorted by stage.");
            descriptor.Field(x => x.GenderRate)
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
            descriptor.Field(x => x.FormsSwitchable)
                .Description("Whether or not this pokémon has multiple forms and can switch between them.");
            descriptor.UseNamedApiResourceField<PokemonSpecies, GrowthRate, GrowthRateType>(x => x.GrowthRate);
            descriptor.Field(x => x.PokedexNumbers)
                .Description("A list of pokedexes and the indexes reserved within them for this pokémon species.")
                .Type<ListType<PokemonSpeciesDexEntryType>>();
            descriptor.UseNamedApiResourceCollectionField<PokemonSpecies, EggGroup, EggGroupType>(x => x.EggGroups);
            descriptor.UseNamedApiResourceField<PokemonSpecies, PokemonColor, PokemonColorType>(x => x.Color);
            descriptor.UseNamedApiResourceField<PokemonSpecies, PokemonShape, PokemonShapeType>(x => x.Shape);
            descriptor.UseNullableNamedApiResourceField<PokemonSpecies, PokemonSpecies, PokemonSpeciesType>(x => x.EvolvesFromSpecies);
            descriptor.UseApiResourceField<PokemonSpecies, EvolutionChain, EvolutionChainType>(x => x.EvolutionChain);
            descriptor.UseNamedApiResourceField<PokemonSpecies, PokemonHabitat, PokemonHabitatType>(x => x.Habitat);
            descriptor.UseNamedApiResourceField<PokemonSpecies, Generation, GenerationType>(x => x.Generation);
            descriptor.Field(x => x.PalParkEncounters)
                .Description("A list of encounters that can be had with this pokémon species in pal park.")
                .Type<ListType<PalParkEncounterAreaType>>();
            descriptor.Field(x => x.Genera)
                .Description("The genus of this pokémon species listed in multiple languages.")
                .Type<ListType<GenusType>>();
            descriptor.Field(x => x.Varieties)
                .Description("A list of the pokémon that exist within this pokémon species.")
                .Type<ListType<PokemonSpeciesVarietyType>>();

            // TODO type should be changed in upstream to {version,language,flavor_text}[]; FlavorTexts is only {flavor_text, language}
            // See https://pokeapi.co/api/v2/pokemon-species/1
            descriptor.Field(x => x.FlavorTextEntries)
                .Type<ListType<FlavorTextType>>();
        }

        private sealed class PokemonSpeciesVarietyType : ObjectType<PokemonSpeciesVariety>
        {
            protected override void Configure(IObjectTypeDescriptor<PokemonSpeciesVariety> descriptor)
            {
                descriptor.Field(x => x.IsDefault);
                descriptor.UseNamedApiResourceField<PokemonSpeciesVariety, Pokemon, PokemonType>(x => x.Pokemon);
            }
        }

        private sealed class PokemonSpeciesDexEntryType : ObjectType<PokemonSpeciesDexEntry>
        {
            protected override void Configure(IObjectTypeDescriptor<PokemonSpeciesDexEntry> descriptor)
            {
                descriptor.Field(x => x.EntryNumber)
                    .Description("The index number within the pokédex.");
                descriptor.UseNamedApiResourceField<PokemonSpeciesDexEntry, Pokedex, PokedexType>(x => x.Pokedex);
            }
        }

        private sealed class GenusType : ObjectType<Genuses>
        {
            protected override void Configure(IObjectTypeDescriptor<Genuses> descriptor)
            {
                descriptor.Field(x => x.Genus)
                    .Description("The localized genus for the referenced pokémon species.");
                descriptor.UseNamedApiResourceField<Genuses, Language, LanguageType>(x => x.Language);
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
                descriptor.UseNamedApiResourceField<PalParkEncounterArea, PalParkArea, PalParkAreaType>(x => x.Area);
            }
        }
    }
}
