import React, { useState, useEffect } from 'react';
import { DataView, DataViewLayoutOptions } from 'primereact/dataview';
import { Button } from 'primereact/button';
import { Dropdown } from 'primereact/dropdown';
import { Skeleton } from 'primereact/skeleton';
import { classNames } from 'primereact/utils';
import { getPokemons } from '../api/pokemonApi.js';

export default function ShopItems() {
    const [products, setProducts] = useState([]);
    const [layout, setLayout] = useState('grid');
    const [loading, setLoading] = useState(true);

    const [savedProducts, setSavedProducts] = useState([]);
    const [sortKey, setSortKey] = useState('id');
    const [filterKey, setFilterKey] = useState('');


    const [sortOrder, setSortOrder] = useState(0);
    const [sortField, setSortField] = useState('');
    const sortOptions = [
        { label: 'Pokedex Number', value: "id" },
        { label: 'Price High to Low', value: '!price' },
        { label: 'Price Low to High', value: 'price' }
    ];

    const filterOptions = [
        { label: "None", value: "" },   
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
        { label: "Fairy", value: "Fairy" },
    ];

    useEffect(() => {
        setLoading(true);
        getPokemons().then(data => {
            setProducts(data);
            setSavedProducts(data)
            setLoading(false);
        });
    }, []);

    const onSortChange = (event) => {
        const value = event.value;

        if (value === 'id') {
            setSortKey('id');
            setSortOrder(0);
            setSortField('');
            return;
        } else if (value.indexOf('!') === 0) {
            setSortOrder(-1);
            setSortField(value.substring(1));
            setSortKey(value);
        } else {
            setSortOrder(1);
            setSortField(value);
            setSortKey(value);
        }
    };

    const onFilterChange = (event) => {
        const value = event.value;

        setFilterKey(value)

        if (value.value === '') {
            setProducts(savedProducts)
        }
        else {
            setProducts(savedProducts.filter((pokemon) => pokemon.type1 === value))
        }
    };

    const listSkeleton = () => (
        <div className="col-12">
            <div className="flex flex-column xl:flex-row xl:align-items-start p-4 gap-4 border-top-1 surface-border">
                <Skeleton className="w-9 sm:w-16rem xl:w-10rem shadow-2 h-6rem block border-round" />
                <div className="flex flex-column sm:flex-row justify-content-between flex-1 gap-4">
                    <div className="flex flex-column gap-3">
                        <Skeleton className="w-8rem h-2rem" />
                        <Skeleton className="w-6rem h-1rem" />
                    </div>
                    <div className="flex flex-column align-items-end gap-3">
                        <Skeleton className="w-4rem h-2rem" />
                        <Skeleton shape="circle" className="w-3rem h-3rem" />
                    </div>
                </div>
            </div>
        </div>
    );

    const gridSkeleton = () => (
        <div className="col-12 sm:col-6 lg:col-12 xl:col-4 p-2">
            <div className="p-4 border-1 surface-border surface-card border-round">
                <Skeleton className="w-6rem h-1rem" />
                <div className="flex flex-column align-items-center gap-3 py-5">
                    <Skeleton className="w-9 h-10rem border-round" />
                    <Skeleton className="w-8rem h-2rem" />
                    <Skeleton className="w-6rem h-1rem" />
                </div>
                <div className="flex justify-content-between">
                    <Skeleton className="w-4rem h-2rem" />
                    <Skeleton shape="circle" className="w-3rem h-3rem" />
                </div>
            </div>
        </div>
    );

    const listItem = (product, index) => {
        return (
            <div className="col-12" key={product.id}>
                <div className={classNames('flex flex-column xl:flex-row xl:align-items-start p-4 gap-4', { 'border-top-1 surface-border': index !== 0 })}>
                    <img className="w-9 sm:w-16rem xl:w-10rem shadow-2 block mx-auto border-round" src={product.sprite} alt={product.name} />

                    <div className="flex flex-column sm:flex-row justify-content-between flex-1 gap-4">
                        <div className="flex flex-column gap-3">
                            <div className="text-2xl font-bold">{product.name}</div>
                            <div className="flex gap-3">
                                <i className="pi pi-tag"></i>
                                <span>{product.type1} {product.type2}</span>
                            </div>
                        </div>

                        <div className="flex flex-column align-items-end gap-3">
                            <span className="text-2xl font-semibold">${product.price}</span>
                            <Button icon="pi pi-shopping-cart" className="p-button-rounded" />
                        </div>
                    </div>
                </div>
            </div>
        );
    };

    const gridItem = (product) => {
        return (
            <div className="col-12 sm:col-6 lg:col-12 xl:col-4 p-2" key={product.id}>
                <div className="p-4 border-1 surface-border surface-card border-round">
                    <div className="flex align-items-center gap-2">
                        <i className="pi pi-tag"></i>
                        <span>{product.type1} {product.type2}</span>
                    </div>

                    <div className="flex flex-column align-items-center gap-3 py-5">
                        <img className="w-9 shadow-2 border-round" src={product.sprite} alt={product.name} />
                        <div className="text-2xl font-bold">{product.name}</div>
                    </div>

                    <div className="flex justify-content-between">
                        <span className="text-2xl font-semibold">${product.price}</span>
                        <Button icon="pi pi-shopping-cart" className="p-button-rounded" />
                    </div>
                </div>
            </div>
        );
    };

    const itemTemplate = (product, layout, index) => {
        if (loading) {
            return layout === 'list' ? listSkeleton() : gridSkeleton();
        }

        if (!product) return;

        return layout === 'list'
            ? listItem(product, index)
            : gridItem(product);
    };

    const listTemplate = (items, layout) => {
        const displayItems = loading
            ? Array.from({ length: 6 })
            : items;

        return (
            <div className="grid grid-nogutter">
                {displayItems.map((item, i) => itemTemplate(item, layout, i))}
            </div>
        );
    };

    const header = () => (
        <div className="flex justify-content-between align-items-center">
            <Dropdown options={sortOptions} value={sortKey} optionLabel="label"
                placeholder="Sort By..." onChange={onSortChange} className="w-full sm:w-14rem" />

            <Dropdown options={filterOptions} value={filterKey} optionLabel="label"
                placeholder="Filter type..." onChange={onFilterChange} className="w-full sm:w-14rem" />

            <DataViewLayoutOptions layout={layout} onChange={(e) => setLayout(e.value)} />
        </div>
    );

    return (
        <div className="card">
            <DataView
                value={products}
                listTemplate={listTemplate}
                layout={layout}
                paginator
                rows={30}
                totalRecord={ }
                header={header()}
                sortField={sortField}
                sortOrder={sortOrder}
            />
        </div>
    );
}
