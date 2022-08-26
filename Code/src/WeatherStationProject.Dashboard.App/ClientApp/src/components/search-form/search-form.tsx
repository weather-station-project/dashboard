import React from 'react';
import { useTranslation } from 'react-i18next';
import {
  ChartValues,
  DefaultHistoricalDataRequest,
  GroupingValues,
  HistoricalDataRequestValidationSchema,
  IHistoricalDataRequest,
} from '../../model/HistoricalDataTypes';
import { Field, FieldProps, Formik, FormikHelpers } from 'formik';
import { Button, Col, Form, Row } from 'react-bootstrap';

interface ISearchFormProps {
  showChartView: boolean;
  onSubmit: (values: IHistoricalDataRequest, helpers: FormikHelpers<IHistoricalDataRequest>) => void;
}

const SearchForm: React.FC<ISearchFormProps> = ({ showChartView, onSubmit }) => {
  const { t } = useTranslation();

  return (
    <Formik
      validationSchema={HistoricalDataRequestValidationSchema}
      onSubmit={onSubmit}
      initialValues={DefaultHistoricalDataRequest}
      validateOnChange={false}
    >
      {({ handleSubmit, handleChange, values, touched, errors, isSubmitting }) => (
        <Form noValidate onSubmit={handleSubmit}>
          <Row className="mb-3 mt-5">
            <Form.Group as={Col} controlId="initialDate">
              <Form.Label>{t('historical_data.initial_date')}</Form.Label>
              <Form.Control
                type="date"
                name="initialDate"
                onChange={handleChange}
                isValid={touched.initialDate && !errors.initialDate}
                isInvalid={!!errors.initialDate}
                disabled={isSubmitting}
              />
              <Form.Control.Feedback type="invalid">
                {!!errors.initialDate && t(errors.initialDate)}
              </Form.Control.Feedback>
            </Form.Group>
            <Form.Group as={Col} controlId="finalDate">
              <Form.Label>{t('historical_data.final_date')}</Form.Label>
              <Form.Control
                type="date"
                name="finalDate"
                onChange={handleChange}
                isValid={touched.finalDate && !errors.finalDate}
                isInvalid={!!errors.finalDate}
                disabled={isSubmitting}
              />
              <Form.Control.Feedback type="invalid">{!!errors.finalDate && t(errors.finalDate)}</Form.Control.Feedback>
            </Form.Group>
          </Row>
          <Row className="mb-3">
            {showChartView ? (
              <Form.Group as={Col} controlId="chartView">
                <Form.Label>{t('historical_data.chart_view')}</Form.Label>
                <Field name="chartView">
                  {({ field, form: { setFieldValue } }: FieldProps) => (
                    <>
                      <Form.Check
                        type="radio"
                        id="chartView-lines"
                        checked={values.chartView === ChartValues.Lines}
                        name="chartView"
                        label={t('historical_data.chart_view.lines')}
                        onChange={() => setFieldValue(field.name, ChartValues.Lines, false)}
                        disabled={isSubmitting}
                      />
                      <Form.Check
                        type="radio"
                        id="chartView-bars"
                        checked={values.chartView === ChartValues.Bars}
                        name="chartView"
                        label={t('historical_data.chart_view.bars')}
                        onChange={() => setFieldValue(field.name, ChartValues.Bars, false)}
                        disabled={isSubmitting}
                      />
                    </>
                  )}
                </Field>
                <Form.Control.Feedback type="invalid">
                  {!!errors.chartView && t(errors.chartView)}
                </Form.Control.Feedback>
              </Form.Group>
            ) : (
              <Col />
            )}
            <Form.Group as={Col} controlId="grouping">
              <Form.Label>{t('historical_data.grouping')}</Form.Label>
              <Field name="grouping">
                {({ field, form: { setFieldValue } }: FieldProps) => (
                  <Form.Select
                    className={'form-control'}
                    isValid={touched.grouping && !errors.grouping}
                    isInvalid={!!errors.grouping}
                    onChange={(option) => setFieldValue(field.name, option.target.value, false)}
                    disabled={isSubmitting}
                  >
                    <option value={undefined}>{t('historical_data.grouping.select')}</option>
                    <option value={GroupingValues.Hours}>{t('historical_data.grouping.hours')}</option>
                    <option value={GroupingValues.Days}>{t('historical_data.grouping.days')}</option>
                    <option value={GroupingValues.Months}>{t('historical_data.grouping.months')}</option>
                  </Form.Select>
                )}
              </Field>
              <Form.Control.Feedback type="invalid">{!!errors.grouping && t(errors.grouping)}</Form.Control.Feedback>
            </Form.Group>
          </Row>
          <Row>
            <Col>
              <Button type="submit" disabled={isSubmitting}>
                {t('historical_data.submit')}
              </Button>
            </Col>
          </Row>
        </Form>
      )}
    </Formik>
  );
};

export default SearchForm;