<template>
  <div>
    <router-link to="/">Home</router-link>
    <h1>Transaction History</h1>
    <div v-if="transactionData != null">
      <v-data-table
        :headers="headers"
        :items="transactionData"
        :items-per-page="5"
        class="elevation-1"
      ></v-data-table>
    </div>
  </div>
</template>

<script>
import TransactionService from '@/services/transaction.service'
export default {
  name: 'TransactionHistory',
  data () {
    return {
      headers: [
        // {
        //   text: 'Id',
        //   align: 'start',
        //   sortable: false,
        //   value: 'name'
        // },
        { text: 'Account ID', value: 'accountId' },
        { text: 'Transaction Date', value: 'transDate' },
        { text: 'Amount', value: 'amount' },
        { text: 'Transfer Type', value: 'transType' },
        { text: 'Type', value: 'type' },
        { text: 'Description', sortable: false, value: 'desc' }
      ],
      transactionData: null,
      loading: true,
      errored: false
    }
  },
  mounted () {
    TransactionService.getAll()
      .then(response => (this.transactionData = response.data))
      .catch(error => {
        console.log(error)
        this.errored = false
      })
      .finally(() => { this.loading = false })
  }
}
</script>

<style scoped>
</style>
