/* eslint-disable */
import Axios from 'axios';

const RESOURCE_NAME = '/api/Accounts';

export default {
    getAll() {
        return Axios.get(RESOURCE_NAME);
    },

    getAccount(id) {
        return Axios.get(`${RESOURCE_NAME}/${id}`)
    },

    postNewAccount(newAccount) {
        return Axios.post(RESOURCE_NAME, newAccount)
    },

    putUpdate(id, newData) {
        return Axios.put(`${RESOURCE_NAME}/${id}`, newData)
    },

    deleteAccount(id) {
        return Axios.delete(`${RESOURCE_NAME}/${id}`)
    }
}