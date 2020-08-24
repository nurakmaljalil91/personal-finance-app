<template>
  <div>
    <v-sheet>
      <h1>New Transaction</h1>
      <v-row>
        <v-col lg="3"  md="3" sm="12" cols="12" class="align-self-start"><h2>Account ID</h2></v-col>
        <v-col lg="9"  md="9" sm="12" cols="12" class="align-self-center">
          <v-select label="Account ID" :items="accountList" v-model="transctionData.AccountId" filled outlined></v-select>
        </v-col>
        <v-col lg="3"  md="3" sm="12" cols="12" class="align-self-start"><h2>Transfer Type</h2></v-col>
        <v-col lg="9"  md="9" sm="12" cols="12" class="align-self-center">
          <v-select label="Type" :items="transferTypes" v-model="transterTypeSelection"  filled outlined></v-select>
        </v-col>
        <v-col lg="3"  md="3" sm="12" cols="12" class="align-self-start"><h2>Amount</h2></v-col>
        <v-col lg="9"  md="9" sm="12" cols="12" class="align-self-center">
          <v-text-field label="Amount" v-model="transctionDat.amount" single-line outlined></v-text-field>
        </v-col>
        <v-col lg="3"  md="3" sm="12" cols="12" class="align-self-start"><h2>Type</h2></v-col>
        <v-col lg="9"  md="9" sm="12" cols="12" class="align-self-center">
          <v-text-field label="Type" v-model="transactionData.Type" single-line outlined></v-text-field>
        </v-col>
        <v-col lg="3"  md="3" sm="12" cols="12" class="align-self-start"><h2>Description</h2></v-col>
        <v-col lg="9"  md="9" sm="12" cols="12" class="align-self-center">
          <v-text-field label="Description" v-model="transactionData.Desc" single-line outlined></v-text-field>
        </v-col>
        <v-col cols="6"  class="align-self-center"> <v-btn color="primary" dark @click="submitForm">Submit</v-btn></v-col>
        <v-col cols="6"  class="align-self-center"> <v-btn color="warning" dark>Clear</v-btn></v-col>
      </v-row>
    </v-sheet>
  </div>
</template>

<script>
import AccountService from '@/services/account.service'
import TransactionService from '@/services/transaction.service'
export default {
  name: 'TransactionForm',
  data () {
    return {
      account: null,
      transferTypes: ['Debit', 'Credit'],
      transterTypeSelection: null,
      accountList: [],
      transctionData: {
        Id: '',
        AccountId: '',
        TransDate: new Date(),
        amount: '',
        TransType: null,
        Type: '',
        Desc: ''
      }
    }
  },
  mounted () {
    AccountService.getAll()
      .then(response => {
        this.account = response.data

        this.account.forEach(e => {
          this.accountList.push(e.id)
        })
      })
      .catch(error => {
        console.log(error)
        this.errored = true
      })
      .finally(() => {
        this.loading = false
      })
  },
  methods: {
    submitForm () {
      var today = new Date()
      var dd = String(today.getDate()).padStart(2, '0')
      var mm = String(today.getMonth() + 1).padStart(2, '0')
      var yyyy = today.getFullYear()
      today = yyyy + '-' + mm + '-' + dd
      var iddate = dd + mm + yyyy + String(Math.floor(Math.random() * 101))
      this.transctionData.Id = iddate
      this.transctionData.TransDate = today
      this.transctionData.amount = parseFloat(this.transctionData.amount)
      if (this.transterTypeSelection === 'Debit') {
        this.transctionData.TransType = 1
      } else {
        this.transctionData.TransType = 2
      }
      console.log(this.transctionData)
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
