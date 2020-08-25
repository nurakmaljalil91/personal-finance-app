<template>
  <div>
    <v-card class="mx-auto" max-width="500" outlined>
      <v-list-item three-line>
        <v-list-item-content>
          <div class="overline mb-4">{{ accounts.id}}</div>
          <v-list-item-title class="headline mb-1">{{ accounts.balance }}</v-list-item-title>
          <v-list-item-subtitle>{{ accounts.latestDate }}</v-list-item-subtitle>
        </v-list-item-content>
        <v-list-item-avatar tile size="80" color="grey"></v-list-item-avatar>
      </v-list-item>
      <v-card-actions>
        <v-btn text>Transaction Details</v-btn>
      </v-card-actions>
    </v-card>
  </div>
</template>

<script>
export default {
  name: 'SummaryCard',
  props: [
    'accounts'
  ]
}
</script>

<style scoped>
</style>
