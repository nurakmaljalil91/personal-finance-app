/* eslint-disable */
import Axios from 'axios';

const RESOURCE_NAME = '/api/Accounts';

export default {
    getAll() {
        return Axios.get(RESOURCE_NAME);
    },

    postNewAccount(newAccount) {
        return Axios.post(RESOURCE_NAME, newAccount)
    },

    putUpdate(newData) {
        return Axios.put(RESOURCE_NAME, newData)
    }


};