import api from "./axios.js";

//GET ALL
export const getPokemons = async () => {
    const res = await api.get ("/Pokemon");
    return res.data;
};

// GET ALL FROM A PAGE
export const getPaginatedPokemon = async (pageNumber, pageSize, filterType, sort) => {
    const res = await api.get(`/Pokemon/${pageNumber}/${pageSize}/${filterType}/${sort}`)
    return res.data
}