<template>
  <div>
    <v-sheet>
      <h1>New Transaction</h1>
      <v-row>
        <v-col lg="3"  md="3" sm="12" cols="12" class="align-self-start"><h2>Account ID</h2></v-col>
        <v-col lg="9"  md="9" sm="12" cols="12" class="align-self-center">
          <v-select label="Account ID" :items="accountList" filled outlined></v-select>
        </v-col>
        <v-col lg="3"  md="3" sm="12" cols="12" class="align-self-start"><h2>Transfer Type</h2></v-col>
        <v-col lg="9"  md="9" sm="12" cols="12" class="align-self-center">
          <v-select label="Type" :items="transferTypes"  filled outlined></v-select>
        </v-col>
        <v-col lg="3"  md="3" sm="12" cols="12" class="align-self-start"><h2>Amount</h2></v-col>
        <v-col lg="9"  md="9" sm="12" cols="12" class="align-self-center">
          <v-text-field label="Amount" single-line outlined></v-text-field>
        </v-col>
        <v-col lg="3"  md="3" sm="12" cols="12" class="align-self-start"><h2>Type</h2></v-col>
        <v-col lg="9"  md="9" sm="12" cols="12" class="align-self-center">
          <v-text-field label="Type" single-line outlined></v-text-field>
        </v-col>
        <v-col lg="3"  md="3" sm="12" cols="12" class="align-self-start"><h2>Description</h2></v-col>
        <v-col lg="9"  md="9" sm="12" cols="12" class="align-self-center">
          <v-text-field label="Description" single-line outlined></v-text-field>
        </v-col>
        <v-col cols="6"  class="align-self-center"> <v-btn color="primary" dark>Submit</v-btn></v-col>
        <v-col cols="6"  class="align-self-center"> <v-btn color="warning" dark>Clear</v-btn></v-col>
      </v-row>
    </v-sheet>
  </div>
</template>

<script>
export default {
  name: 'TransactionForm',
  data () {
    return {
      transferTypes: ['Debit', 'Credit'],
      accountList: ['MY151', 'WLL001', 'TNG03'],
      transctionData: {
        Id: '',
        AccountId: '',
        TransDate: new Date(),
        amount: '',
        TransType: '',
        Type: '',
        Desc: ''
      }
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
