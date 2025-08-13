import { API_URL } from "../../../config/api";

export const propertyService = {
  async getAll(params) {
    const query = new URLSearchParams(params).toString();
    console.log("Llamando a:", `${API_URL}/Properties?${query}`);

    const res = await fetch(`${API_URL}/Properties?${query}`);
    if (!res.ok) {
      throw new Error(`Error HTTP: ${res.status}`);
    }

    return res.json();
  },

   async getById(id) {
    const res = await fetch(`${API_URL}/Properties/${id}`);
    if (!res.ok) throw new Error(`Error HTTP: ${res.status}`);
    return res.json();
  }
};
