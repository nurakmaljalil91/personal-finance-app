<template>
  <div>
    <div v-if="loading" class="loading">
      <v-progress-circular indeterminate color="primary"></v-progress-circular>
      <p>loading...</p>
    </div>
    <div v-if="errored" class="loading">
      <v-icon color="red">fas fa-exclamation-triangle</v-icon>
      <p>error...</p>
    </div>
    <div v-if="total != null">
      <h1>Total Amount: {{ total }} MYR</h1>
    </div>
  </div>
</template>

<script>
import Account from '@/services/account.service'
export default {
  name: 'Total',
  data () {
    return {
      account: null,
      loading: true,
      errored: false,
      sum: [],
      total: null
    }
  },
  mounted () {
    Account.getAll()
      .then(response => {
        this.account = response.data

        this.account.forEach(element => {
          this.sum.push(element.balance)
        })
        this.total = this.sum.reduce(function (a, b) {
          return a + b
        }, 0)
        this.total = this.total.toFixed(2)
      })
      .catch(error => {
        console.log(error)
        this.errored = true
      })
      .finally(() => {
        this.loading = false
      })
  }
}
</script>

<style scoped></style>
