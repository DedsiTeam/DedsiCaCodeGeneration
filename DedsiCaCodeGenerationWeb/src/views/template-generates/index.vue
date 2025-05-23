<template>
<section id="layout-section">
  <section id="header-section">
    <div class="content-div">
      <div style="text-align: center;font-size: 1.4rem; padding: 1rem 0;">Dedsi DDD CodeGeneration</div>
      <a-form :style="{ width: '50%', margin: '0px auto'}" :model="formData" @submit="handleSubmit">
        <a-form-item field="projectName" label="项目名称" :rules="[{required:true,message:'请输入项目名称'}]" :validate-trigger="['change','input']">
          <a-input v-model="formData.projectName" placeholder="请输入项目名称" />
        </a-form-item>
        <a-form-item field="domainName" label="领域名称" :rules="[{required:true,message:'请输入领域名称'}]" :validate-trigger="['change','input']">
          <a-input v-model="formData.domainName" placeholder="请输入领域名称" />
        </a-form-item>
        <a-form-item>
          <a-space>
            <a-button type="primary" html-type="submit">生成</a-button>
          </a-space>
        </a-form-item>
      </a-form>
    </div>
  </section>
  <section id="body-section" v-if="showBody">
    <div class="content-div">
      <a-tabs default-active-key="Domain">
        <a-tab-pane key="Domain" title="Domain">
          <div class="code-div"><code class="ddd-code"><pre>{{ templateGenerateData.domain }}</pre></code></div>
        </a-tab-pane>
        <a-tab-pane key="CreateCommandHandler" title="CreateCommandHandler">
          <div class="code-div"><code class="ddd-code"><pre>{{ templateGenerateData.createCommandHandler }}</pre></code></div>
        </a-tab-pane>
        <a-tab-pane key="UpdateCommandHandler" title="UpdateCommandHandler">
          <div class="code-div"><code class="ddd-code"><pre>{{ templateGenerateData.updateCommandHandler }}</pre></code></div>
        </a-tab-pane>
        <a-tab-pane key="DeleteCommandHandler" title="DeleteCommandHandler">
          <div class="code-div"><code class="ddd-code"><pre>{{ templateGenerateData.deleteCommandHandler }}</pre></code></div>
        </a-tab-pane>
        <a-tab-pane key="EfCoreRepository" title="EfCoreRepository">
          <div class="code-div"><code class="ddd-code"><pre>{{ templateGenerateData.efCoreRepository }}</pre></code></div>
        </a-tab-pane>
        <a-tab-pane key="EfCoreQuery" title="EfCoreQuery">
          <div class="code-div"><code class="ddd-code"><pre>{{ templateGenerateData.efCoreQuery }}</pre></code></div>
        </a-tab-pane>
        <a-tab-pane key="Dtos" title="Dtos">
          <div class="code-div"><code class="ddd-code"><pre>{{ templateGenerateData.dtos }}</pre></code></div>
        </a-tab-pane>
        <a-tab-pane key="Controller" title="Controller">
          <div class="code-div"><code class="ddd-code"><pre>{{ templateGenerateData.controller }}</pre></code></div>
        </a-tab-pane>
      </a-tabs>
    </div>
  </section>
</section>
</template>

<script setup>
import { reactive, ref } from 'vue';
import axios from 'axios';

const formData = reactive({
  projectName: '',
  domainName: '',
})

const showBody = ref(false);
const templateGenerateData = ref({
  controller:'',
  createCommandHandler:'',
  updateCommandHandler:'',
  deleteCommandHandler:'',
  domain: '',
  dtos: '',
  efCoreQuery: '',
  efCoreRepository: ''
})

async function handleSubmit({values, errors}) {
  showBody.value = false;
  if (errors === undefined) {
    const axiosData = await axios.post(import.meta.env.VITE_DedsiCaCodeGenerationApi + 'api/code-generation/template/generate', values);
    templateGenerateData.value = axiosData.data;
    showBody.value = true;
  }
}
</script>

<style scoped>
:deep(div.arco-tabs-content){
  padding-top: 0;
}
section#layout-section {
  width: 100dvw;
  height: 100vh;
  overflow: hidden;
  background-color: #EFF1F5;

}

section#header-section{
  width: 100vw;
  height: 214px;
  background-color: white;
}
section#body-section {
  width: 100vw;
  height: calc(100vh - 214px);
  overflow-y: auto;
  padding-bottom: 2rem;
}

.content-div {
  width: 80%;
  margin: 0 auto;
}

div.code-div {
  background-color: #fff;
  padding: 1rem;
  box-sizing: border-box;
  font-size: 1rem;
}
code.ddd-code > pre {
  margin: 0;
}
</style>