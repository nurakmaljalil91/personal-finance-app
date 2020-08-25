<template>
  <div>
    <v-row>
      <v-col>
        <router-link to="/AddAccount">Add Account</router-link>
      </v-col>
      <v-col>
        <router-link to="/AddTransaction">Add Transaction</router-link>
      </v-col>
    </v-row>
    <div v-if="loading" class="loading">
      <v-progress-circular indeterminate color="primary"></v-progress-circular>
      <p>loading...</p>
    </div>
    <div v-if="errored" class="loading">
      <v-icon color="red">fas fa-exclamation-triangle</v-icon>
      <p>error...</p>
    </div>
    <div v-else v-for="acc in accounts" v-bind:key="acc.id">
      <v-lazy
        :options="{
          threshold: .5
        }"
        min-height="200"
        transition="fade-transition"
      >
        <SummaryCard v-bind:accounts="acc" />
      </v-lazy>
    </div>
    <SummaryCard />
  </div>
</template>

<script>
import SummaryCard from '@/components/SummaryCard'
import AccountService from '@/services/account.service'
export default {
  name: 'Home',
  components: {
    SummaryCard
  },
  data () {
    return {
      accounts: null,
      loading: true,
      errored: false
    }
  },
  mounted () {
    AccountService.getAll()
      .then(response => (this.accounts = response.data))
      .catch(error => {
        console.log(error)
        this.errored = true
      })
      .finally(() => { this.loading = false })
  }
}
</script>

<style scoped>
</style>
