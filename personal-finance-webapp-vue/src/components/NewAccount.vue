<template>
  <div>
    <v-sheet>
      <h1>New Account</h1>
      <v-row>
        <v-col lg="3"  md="3" sm="12" cols="12" class="align-self-start"><h2>Account ID</h2></v-col>
        <v-col lg="9"  md="9" sm="12" cols="12" class="align-self-center">
          <v-text-field label="Account ID" v-model="newAccountData.id" single-line outlined></v-text-field>
        </v-col>
        <v-col lg="3"  md="3" sm="12" cols="12" class="align-self-start"><h2>Balance</h2></v-col>
        <v-col lg="9"  md="9" sm="12" cols="12" class="align-self-center">
          <v-text-field label="Balance" v-model="newAccountData.balance" type="number" min="0" single-line outlined></v-text-field>
        </v-col>
        <v-col cols="6"  class="align-self-center"> <v-btn color="primary" dark @click="submitForm">Submit</v-btn></v-col>
        <v-col cols="6"  class="align-self-center"> <v-btn color="warning" dark @click="clearForm">Clear</v-btn></v-col>
      </v-row>
    </v-sheet>
  </div>
</template>

<script>
import AccountService from '@/services/account.service'
export default {
  name: 'NewAccount',
  data () {
    return {
      newAccountData: {
        id: '',
        balance: null,
        latestDate: null
      }
    }
  },
  methods: {
    submitForm () {
      var today = new Date()
      var dd = String(today.getDate()).padStart(2, '0')
      var mm = String(today.getMonth() + 1).padStart(2, '0')
      var yyyy = today.getFullYear()
      today = yyyy + '-' + mm + '-' + dd
      this.newAccountData.latestDate = today
      this.newAccountData.balance = parseFloat(this.newAccountData.balance)
      AccountService.postNewAccount(this.newAccountData)
       .then( this.$alert("New Account Added!", 'Success','success'))
       .catch(error => (this.$alert("Something happened!", 'Warning','warning')))
    },
    clearForm () {
      // eslint-disable-next-line no-unused-expressions
      this.newAccountData.id = ''
      this.newAccountData.balance = ''
      this.newAccountData.latestDate = ''
    }
  },
  computed: {
    FormatDate () {
      var today = new Date()
      var dd = String(today.getDate()).padStart(2, '0')
      var mm = String(today.getMonth() + 1).padStart(2, '0')
      var yyyy = today.getFullYear()
      today = mm + '-' + dd + '-' + yyyy
      return today
    }
  }
}
</script>

<style scoped>
.v-sheet{
    margin-left: 20px;
    margin-right: 20px;
}
</style>
