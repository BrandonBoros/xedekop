import React, { useState, useEffect } from 'react';
import { DataView, DataViewLayoutOptions } from 'primereact/dataview';
import { Button } from 'primereact/button';
import { Dropdown } from 'primereact/dropdown';
import { Skeleton } from 'primereact/skeleton';
import { classNames } from 'primereact/utils';
import { getPaginatedPokemon } from '../api/pokemonApi.js';
import { Paginator } from 'primereact/paginator';
import "../styles/shop.css";

export default function ShopItems() {
    const [numberOfPokemon, setNumberOfPokemon] = useState(1025);

    const pageSize = 30;

    const [first, setFirst] = useState(0);
    const [pageNumber, setPageNumber] = useState(1);

    const [products, setProducts] = useState([]);
    const [layout, setLayout] = useState('grid');
    const [loading, setLoading] = useState(true);

    const [sortKey, setSortKey] = useState("null");
    const [filterKey, setFilterKey] = useState("null");

    const sortOptions = [
        { label: 'Pokedex Number', value: "null" },
        { label: 'Price High to Low', value: 'priceDESC' },
        { label: 'Price Low to High', value: 'priceASC' }
    ];

    const filterOptions = [ /* keeping your list unchanged */
        { label: "None", value: "null" },
        { label: "Normal", value: "Normal" },
        { label: "Fire", value: "Fire" },
        { label: "Water", value: "Water" },
        { label: "Electric", value: "Electric" },
        { label: "Grass", value: "Grass" },
        { label: "Ice", value: "Ice" },
        { label: "Fighting", value: "Fighting" },
        { label: "Poison", value: "Poison" },
        { label: "Ground", value: "Ground" },
        { label: "Flying", value: "Flying" },
        { label: "Psychic", value: "Psychic" },
        { label: "Bug", value: "Bug" },
        { label: "Rock", value: "Rock" },
        { label: "Ghost", value: "Ghost" },
        { label: "Dragon", value: "Dragon" },
        { label: "Dark", value: "Dark" },
        { label: "Steel", value: "Steel" },
        { label: "Fairy", value: "Fairy" }
    ];

    useEffect(() => {
        setLoading(true);
        getPaginatedPokemon(pageNumber, pageSize, filterKey, sortKey).then(data => {
            setProducts(data[0]);
            setNumberOfPokemon(data[1] * pageSize);
            setLoading(false);
        });
    }, []);

    const onSortChange = (event) => {
        const value = event.value;
        setSortKey(value);

        setLoading(true);
        getPaginatedPokemon(pageNumber, pageSize, filterKey, value).then(data => {
            setProducts(data[0]);
            setNumberOfPokemon(data[1] * pageSize);
            setLoading(false);
        });
    };

    const onFilterChange = (event) => {
        const value = event.value;

        setFilterKey(value);
        setLoading(true);

        getPaginatedPokemon(pageNumber, pageSize, value, sortKey).then(data => {
            setProducts(data[0]);
            setNumberOfPokemon(data[1] * pageSize);
            setLoading(false);
        });
    };

    const onPageChange = (event) => {
        setPageNumber(event.page + 1);
        setFirst(event.first);

        setLoading(true);
        getPaginatedPokemon(event.page + 1, pageSize, filterKey, sortKey).then(data => {
            setProducts(data[0]);
            setNumberOfPokemon(data[1] * pageSize);
            setLoading(false);
        });
    };

    /* --- ITEM STYLING --- */

    const listItem = (product, index) => (
        <div className="col-12" key={product.id}>
            <div className={classNames('pokemon-card flex flex-column xl:flex-row xl:align-items-start p-4 gap-4',
                { 'border-top-1 surface-border': index !== 0 })}>

                <img
                    className="w-9 sm:w-16rem xl:w-10rem sprite-hover border-round"
                    src={product.sprite}
                    alt={product.name}
                />

                <div className="flex flex-column sm:flex-row justify-content-between flex-1 gap-4">
                    <div className="flex flex-column gap-3">
                        <div className="text-2xl font-bold">{product.name}</div>

                        <div className="flex gap-2">
                            <span className={`type-chip type-${product.type1}`}>{product.type1}</span>
                            {product.type2 && (
                                <span className={`type-chip type-${product.type2}`}>{product.type2}</span>
                            )}
                        </div>
                    </div>

                    <div className="flex flex-column align-items-end gap-3">
                        <span className="text-2xl font-semibold">${product.price}</span>
                        <Button icon="pi pi-shopping-cart" className="p-button-rounded p-button-sm" />
                    </div>
                </div>
            </div>
        </div>
    );

    const gridItem = (product) => (
        <div className="col-12 sm:col-6 lg:col-4 p-3" key={product.id}>
            <div className="pokemon-card p-4 text-center">

                <div className="flex justify-content-center gap-2 mb-2">
                    <span className={`type-chip type-${product.type1}`}>{product.type1}</span>
                    {product.type2 && (
                        <span className={`type-chip type-${product.type2}`}>{product.type2}</span>
                    )}
                </div>

                <img
                    className="w-9 sprite-hover border-round mb-3"
                    src={product.sprite}
                    alt={product.name}
                />

                <div className="text-2xl font-bold mb-3">{product.name}</div>

                <div className="flex justify-content-between align-items-center">
                    <span className="text-2xl font-semibold">${product.price}</span>
                    <Button icon="pi pi-shopping-cart" className="p-button-rounded p-button-sm" />
                </div>
            </div>
        </div>
    );

    const itemTemplate = (product, layout, index) => {
        if (loading) return <Skeleton className="w-full h-10rem border-round" />;
        if (!product) return;

        return layout === 'list'
            ? listItem(product, index)
            : gridItem(product);
    };

    const header = () => (
        <div className="flex justify-content-between align-items-center mb-3">
            <Dropdown options={sortOptions} value={sortKey} onChange={onSortChange} className="w-full sm:w-14rem" />
            <Dropdown options={filterOptions} value={filterKey} onChange={onFilterChange} className="w-full sm:w-14rem" />
            <DataViewLayoutOptions layout={layout} onChange={(e) => setLayout(e.value)} />
        </div>
    );

    return (
        <div>
            <DataView
                value={products}
                layout={layout}
                header={header()}
                itemTemplate={itemTemplate}
            />

            <Paginator
                first={first}
                rows={pageSize}
                totalRecords={numberOfPokemon}
                onPageChange={onPageChange}
                className="mt-3"
            />
        </div>
    );
}
