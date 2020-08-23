/* eslint-disable */
import Axios from 'axios';

const RESOURCE_NAME = '/api/Transactions';

export default {
    getAll() {
        return Axios.get(RESOURCE_NAME);
    },

    getTransaction(id) {
        return Axios.get(`${RESOURCE_NAME}/${id}`)
    },

    postNewTransaction(newAccount) {
        return Axios.post(RESOURCE_NAME, newAccount)
    },

    putUpdate(newData) {
        return Axios.put(RESOURCE_NAME, newData)
    },

    deleteTransaction(id) {
        return Axios.delete(`${RESOURCE_NAME}/${id}`)
    }
}